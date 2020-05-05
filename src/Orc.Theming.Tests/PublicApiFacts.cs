namespace Orc.Theming.Tests
{
    using System.Runtime.CompilerServices;
    using ApiApprover;
    using NUnit.Framework;

    [TestFixture]
    public class PublicApiFacts
    {
        [Test, MethodImpl(MethodImplOptions.NoInlining)]
        public void Orc_Theming_HasNoBreakingChanges()
        {
            var assembly = typeof(ThemeColorBrush).Assembly;

            PublicApiApprover.ApprovePublicApi(assembly);
        }
    }
}