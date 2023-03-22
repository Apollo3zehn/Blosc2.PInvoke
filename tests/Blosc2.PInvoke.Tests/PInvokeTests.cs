using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Xunit;
using RuntimeEnvironment = Microsoft.DotNet.PlatformAbstractions.RuntimeEnvironment;

namespace Blosc2.PInvoke.Tests
{
    public class PInvokeTests
    {
        [Fact]
        public void CanAccessNativeLib()
        {
            // Arrange
            Directory.EnumerateFiles("./runtimes/", "*blosc2.*", SearchOption.AllDirectories).ToList().ForEach(filePath =>
            {
                if (filePath.Contains(RuntimeEnvironment.RuntimeArchitecture))
                {
                    File.Copy(filePath, Path.GetFileName(filePath), true);
                }
            });

            // Act
            var compressors = Marshal.PtrToStringAnsi(Blosc.blosc2_list_compressors());

            // Assert
            Assert.True(compressors == "blosclz,lz4,lz4hc,lizard,zlib,zstd");
        }
    }
}