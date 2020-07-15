namespace Orc.Theming.Tests
{
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using ApprovalTests;
    using ApprovalTests.Namers;
    using NUnit.Framework;
    using PublicApiGenerator;

    [TestFixture]
    public class PublicApiFacts
    {
        [Test, MethodImpl(MethodImplOptions.NoInlining)]
#pragma warning disable CA1707 // Identifiers should not contain underscores
        public void Orc_Theming_HasNoBreakingChanges()
#pragma warning restore CA1707 // Identifiers should not contain underscores
        {
            var assembly = typeof(ThemeColorBrush).Assembly;

            PublicApiApprover.ApprovePublicApi(assembly);
        }

        internal static class PublicApiApprover
        {
            public static void ApprovePublicApi(Assembly assembly)
            {
                var publicApi = ApiGenerator.GeneratePublicApi(assembly, new ApiGeneratorOptions());
                var writer = new ApprovalTextWriter(publicApi, "cs");
                var approvalNamer = new AssemblyPathNamer(assembly.Location);
                Approvals.Verify(writer, approvalNamer, Approvals.GetReporter());
            }
        }

        internal class AssemblyPathNamer : UnitTestFrameworkNamer
        {
            private readonly string _name;

            public AssemblyPathNamer(string assemblyPath)
            {
                _name = Path.GetFileNameWithoutExtension(assemblyPath);

            }
            public override string Name
            {
                get { return _name; }
            }
        }
    }
}
