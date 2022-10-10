﻿namespace Orc.Theming
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows;
    using System.Windows.Interop;
    using System.Windows.Media.Imaging;
    using Orc.Theming.Win32;
    using PixelFormat = System.Drawing.Imaging.PixelFormat;
    using Point = System.Drawing.Point;

    public static class IconHelper
    {
        public static Icon? ExtractIconFromFile(string filePath)
        {
            ArgumentNullException.ThrowIfNull(filePath);

            try
            {
                var extractor = new IconExtractor(filePath);
                if (extractor.Count == 0)
                {
                    return null;
                }

                var icon = extractor.GetIcon(0);
                return icon;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static BitmapImage? ExtractLargestIconFromFile(string filePath)
        {
#pragma warning disable IDISP001 // Dispose created.
            var icon = ExtractIconFromFile(filePath);
#pragma warning restore IDISP001 // Dispose created.
            if (icon is null)
            {
                return null;
            }

#pragma warning disable IDISP001 // Dispose created.
            var vistaIcon = ExtractVistaIcon(icon);
#pragma warning restore IDISP001 // Dispose created.
            if (vistaIcon is null)
            {
#pragma warning disable IDISP001 // Dispose created.
                var bitmap = ExtractIcon(icon);
#pragma warning restore IDISP001 // Dispose created.
                if (bitmap is null)
                {
                    return null;
                }

                return ToBitmapImageWithTransparency(bitmap);
            }

            return ToBitmapImageWithTransparency(vistaIcon);
        }

        private static Bitmap? ExtractVistaIcon(Icon? icon)
        {
            Bitmap? extractedIcon = null;

            if (icon is null)
            {
                return extractedIcon;
            }

            try
            {
                byte[] srcBuf;

                using (var stream = new MemoryStream())
                {
                    icon.Save(stream);
                    srcBuf = stream.ToArray();
                }

                const int SizeICONDIR = 6;
                const int SizeICONDIRENTRY = 16;
                int iCount = BitConverter.ToInt16(srcBuf, 4);
                for (var iIndex = 0; iIndex < iCount; iIndex++)
                {
                    int width = srcBuf[SizeICONDIR + SizeICONDIRENTRY * iIndex];
                    int height = srcBuf[SizeICONDIR + SizeICONDIRENTRY * iIndex + 1];
                    int bitCount = BitConverter.ToInt16(srcBuf, SizeICONDIR + SizeICONDIRENTRY * iIndex + 6);

                    if (width == 0 && height == 0 && bitCount == 32)
                    {
                        int imageSize = BitConverter.ToInt32(srcBuf, SizeICONDIR + SizeICONDIRENTRY * iIndex + 8);
                        int imageOffset = BitConverter.ToInt32(srcBuf, SizeICONDIR + SizeICONDIRENTRY * iIndex + 12);
                        using (var destStream = new MemoryStream())
                        {
                            using (var writer = new BinaryWriter(destStream))
                            {
                                writer.Write(srcBuf, imageOffset, imageSize);
                                destStream.Seek(0, SeekOrigin.Begin);

                                extractedIcon = new Bitmap(destStream); // This is PNG! :)
                                return extractedIcon;
                            }
                        }
                    }
                }
            }
            catch
            {
                return null;

            }

            return extractedIcon;
        }

        private static Bitmap? ExtractIcon(Icon? icon)
        {
            if (icon is null)
            {
                return null;
            }

            var bitmapSource = Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            if (bitmapSource is null)
            {
                return null;
            }

            return ToBitmap(bitmapSource);
        }

        private static Bitmap ToBitmap(BitmapSource source)
        {
            ArgumentNullException.ThrowIfNull(source);

            var bitmap = new Bitmap(source.PixelWidth, source.PixelHeight, PixelFormat.Format32bppPArgb);
            var data = bitmap.LockBits(new Rectangle(Point.Empty, bitmap.Size), ImageLockMode.WriteOnly, PixelFormat.Format32bppPArgb);
            source.CopyPixels(Int32Rect.Empty, data.Scan0, data.Height * data.Stride, data.Stride);
            bitmap.UnlockBits(data);
            return bitmap;
        }

        private static BitmapImage ToBitmapImageWithTransparency(Bitmap bitmap)
        {
            ArgumentNullException.ThrowIfNull(bitmap);

            using (var stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Png); // Was .Bmp, but this did not show a transparent background.

                stream.Position = 0;

                var result = new BitmapImage();
                result.BeginInit();
                // According to MSDN, "The default OnDemand cache option retains access to the stream until the image is needed."
                // Force the bitmap to load right now so we can dispose the stream.
                result.CacheOption = BitmapCacheOption.OnLoad;
                result.StreamSource = stream;
                result.EndInit();
                result.Freeze();

                return result;
            }
        }

        private class IconExtractor
        {
            ////////////////////////////////////////////////////////////////////////
            // Constants

            // Flags for LoadLibraryEx().

            private const uint LOAD_LIBRARY_AS_DATAFILE = 0x00000002;

            // Resource types for EnumResourceNames().

            private readonly static IntPtr RT_ICON = (IntPtr)3;
            private readonly static IntPtr RT_GROUP_ICON = (IntPtr)14;

            private const int MAX_PATH = 260;

            ////////////////////////////////////////////////////////////////////////
            // Fields

            private byte[][]? _iconData = null;   // Binary data of each icon.

            ////////////////////////////////////////////////////////////////////////
            // Public properties

            /// <summary>
            /// Gets the full path of the associated file.
            /// </summary>
            public string? FileName
            {
                get;
                private set;
            }

            /// <summary>
            /// Gets the count of the icons in the associated file.
            /// </summary>
            public int Count
            {
                get { return _iconData?.Length ?? 0; }
            }

            /// <summary>
            /// Initializes a new instance of the IconExtractor class from the specified file name.
            /// </summary>
            /// <param name="fileName">The file to extract icons from.</param>
            public IconExtractor(string fileName)
            {
                Initialize(fileName);
            }

            /// <summary>
            /// Extracts an icon from the file.
            /// </summary>
            /// <param name="index">Zero based index of the icon to be extracted.</param>
            /// <returns>A System.Drawing.Icon object.</returns>
            /// <remarks>Always returns new copy of the Icon. It should be disposed by the user.</remarks>
            public Icon? GetIcon(int index)
            {
                if (_iconData is null)
                {
                    return null;
                }

                if (index < 0 || Count <= index)
                {
                    throw new ArgumentOutOfRangeException("index");
                }

                // Create an Icon based on a .ico file in memory.

                using (var ms = new MemoryStream(_iconData[index]))
                {
                    return new Icon(ms);
                }
            }

            private void Initialize(string fileName)
            {
                ArgumentNullException.ThrowIfNull(fileName);

                var hModule = IntPtr.Zero;

                try
                {
                    hModule = Kernel32.LoadLibraryEx(fileName, IntPtr.Zero, LOAD_LIBRARY_AS_DATAFILE);
                    if (hModule == IntPtr.Zero)
                    {
                        throw new Win32Exception();
                    }

                    FileName = GetFileName(hModule);

                    // Enumerate the icon resource and build .ico files in memory.

                    var tmpData = new List<byte[]>();

                    ENUMRESNAMEPROC callback = (h, t, name, l) =>
                    {
                        // Refer the following URL for the data structures used here:
                        // http://msdn.microsoft.com/en-us/library/ms997538.aspx

                        // RT_GROUP_ICON resource consists of a GRPICONDIR and GRPICONDIRENTRY's.

                        var dir = GetDataFromResource(hModule, RT_GROUP_ICON, name);

                        // Calculate the size of an entire .icon file.

                        int count = BitConverter.ToUInt16(dir, 4);  // GRPICONDIR.idCount
                        int len = 6 + 16 * count;                   // sizeof(ICONDIR) + sizeof(ICONDIRENTRY) * count
                        for (int i = 0; i < count; ++i)
                        {
                            len += BitConverter.ToInt32(dir, 6 + 14 * i + 8);   // GRPICONDIRENTRY.dwBytesInRes
                        }

                        using (var dst = new BinaryWriter(new MemoryStream(len)))
                        {
                            // Copy GRPICONDIR to ICONDIR.

                            dst.Write(dir, 0, 6);

                            int picOffset = 6 + 16 * count; // sizeof(ICONDIR) + sizeof(ICONDIRENTRY) * count

                            for (int i = 0; i < count; ++i)
                            {
                                // Load the picture.

                                ushort id = BitConverter.ToUInt16(dir, 6 + 14 * i + 12);    // GRPICONDIRENTRY.nID
                                var pic = GetDataFromResource(hModule, RT_ICON, (IntPtr)id);

                                // Copy GRPICONDIRENTRY to ICONDIRENTRY.

                                dst.Seek(6 + 16 * i, SeekOrigin.Begin);

                                dst.Write(dir, 6 + 14 * i, 8);  // First 8bytes are identical.
                                dst.Write(pic.Length);          // ICONDIRENTRY.dwBytesInRes
                                dst.Write(picOffset);           // ICONDIRENTRY.dwImageOffset

                                // Copy a picture.

                                dst.Seek(picOffset, SeekOrigin.Begin);
                                dst.Write(pic, 0, pic.Length);

                                picOffset += pic.Length;
                            }

                            tmpData.Add(((MemoryStream)dst.BaseStream).ToArray());
                        }

                        return true;
                    };

                    Kernel32.EnumResourceNames(hModule, RT_GROUP_ICON, callback, IntPtr.Zero);

                    _iconData = tmpData.ToArray();
                }
                finally
                {
                    if (hModule != IntPtr.Zero)
                    {
                        Kernel32.FreeLibrary(hModule);
                    }
                }
            }

            private byte[] GetDataFromResource(IntPtr hModule, IntPtr type, IntPtr name)
            {
                // Load the binary data from the specified resource.

                IntPtr hResInfo = Kernel32.FindResource(hModule, name, type);
                if (hResInfo == IntPtr.Zero)
                {
                    throw new Win32Exception();
                }

                IntPtr hResData = Kernel32.LoadResource(hModule, hResInfo);
                if (hResData == IntPtr.Zero)
                {
                    throw new Win32Exception();
                }

                IntPtr pResData = Kernel32.LockResource(hResData);
                if (pResData == IntPtr.Zero)
                {
                    throw new Win32Exception();
                }

                uint size = Kernel32.SizeofResource(hModule, hResInfo);
                if (size == 0)
                {
                    throw new Win32Exception();
                }

                byte[] buf = new byte[size];
                Marshal.Copy(pResData, buf, 0, buf.Length);

                return buf;
            }

            private string GetFileName(IntPtr hModule)
            {
                // Alternative to GetModuleFileName() for the module loaded with
                // LOAD_LIBRARY_AS_DATAFILE option.

                // Get the file name in the format like:
                // "\\Device\\HarddiskVolume2\\Windows\\System32\\shell32.dll"

                string fileName;
                {
                    var buf = new StringBuilder(MAX_PATH);
                    int len = Psapi.GetMappedFileName(Kernel32.GetCurrentProcess(), hModule, buf, buf.Capacity);
                    if (len == 0)
                    {
                        throw new Win32Exception();
                    }

                    fileName = buf.ToString();
                }

                // Convert the device name to drive name like:
                // "C:\\Windows\\System32\\shell32.dll"

                for (char c = 'A'; c <= 'Z'; ++c)
                {
                    var drive = c + ":";
                    var buf = new StringBuilder(MAX_PATH);
                    int len = Kernel32.QueryDosDevice(drive, buf, buf.Capacity);
                    if (len == 0)
                    {
                        continue;
                    }

                    var devPath = buf.ToString();
                    if (fileName.StartsWith(devPath))
                    {
                        return (drive + fileName.Substring(devPath.Length));
                    }
                }

                return fileName;
            }
        }
    }
}
