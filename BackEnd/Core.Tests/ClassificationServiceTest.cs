using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Core.Tests
{
    public class ClassificationServiceTest
    {
        [Fact]
        public async Task ClassifyImageCharacters()
        {
            FormFile file;
            using (var stream = File.OpenRead(@"E:\AOIS\AOIS3.Prep\AOIS3.Prep\Cyrillic\25_3.bmp"))
            {
                stream.Position = 0;
                file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/bmp"
                };
            }
            ClassificationService service = new ClassificationService();
            string result = await service.ClassifyImageCharacters(file);
            Assert.Equal("х", result);
        }
    }
}
