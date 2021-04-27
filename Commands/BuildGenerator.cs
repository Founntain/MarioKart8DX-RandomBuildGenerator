using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace mk8bot.Commands{
    public sealed class BuildGenerator{
        private readonly int DefaultHeight = 128;
        private readonly int DefaultCharSize = 128;
        private readonly int DefaultPartSize = 200;

        public MemoryStream Generate(){
            var stream = new MemoryStream();

            var character = Image.FromFile(GetRandomCharacter());
            var vehicle = Image.FromFile(GetRandomVehicle());
            var tires = Image.FromFile(GetRandomTire());
            var glider = Image.FromFile(GetRandomGlider());

            using(var bitmap = new Bitmap(DefaultCharSize + DefaultPartSize * 3, DefaultHeight * 2)){
                using(var g = Graphics.FromImage(bitmap)){
                    g.DrawImage(character, 0, 0);
                    g.DrawImage(vehicle, 0, DefaultHeight);
                    g.DrawImage(tires, DefaultPartSize, DefaultHeight);
                    g.DrawImage(glider, DefaultPartSize * 2, DefaultHeight);
                }

                bitmap.Save(stream, ImageFormat.Png);
                
                return stream;
            };
        }

        public async Task<MemoryStream> Generate(int amount){
            return await Task.Run(() => {
                var stream = new MemoryStream();

                using(var bitmap = new Bitmap(DefaultPartSize * 3, (DefaultHeight * 2) * amount)){
                    using(var g = Graphics.FromImage(bitmap)){
                        for(var y = 0; y < amount; y++){

                            var character = Image.FromFile(GetRandomCharacter());
                            var vehicle = Image.FromFile(GetRandomVehicle());
                            var tires = Image.FromFile(GetRandomTire());
                            var glider = Image.FromFile(GetRandomGlider());

                            g.DrawString($"Build {y+1}", new Font("Arial", 35f, FontStyle.Bold), Brushes.White, new PointF(DefaultPartSize / 8 + DefaultPartSize, DefaultHeight / 4 + (DefaultHeight * 2) * y));

                            g.DrawImage(character, 0, 0 + (DefaultCharSize * 2) * y);
                            g.DrawImage(vehicle, 0, DefaultHeight + ((DefaultHeight * 2) * y));
                            g.DrawImage(tires, DefaultPartSize, DefaultHeight + ((DefaultHeight * 2) * y));
                            g.DrawImage(glider, DefaultPartSize * 2, DefaultHeight + ((DefaultHeight * 2) * y));
                        }   
                    }

                    bitmap.Save(stream, ImageFormat.Png);
                    
                    return stream;
                };
            });
        }

        private string GetRandomImageFromDirectory(string dir){
            var files = Directory.GetFiles(dir);
            var random = new Random();
            var value = random.Next(0, files.Length - 1);

            return files[value];

        }

        private string GetRandomCharacter(){
            return GetRandomImageFromDirectory("resources/chars");
        }

        private string GetRandomVehicle(){
            return GetRandomImageFromDirectory("resources/parts/vehicles");
        }

        private string GetRandomTire(){
            return GetRandomImageFromDirectory("resources/parts/tires");
        }

        private string GetRandomGlider(){
            return GetRandomImageFromDirectory("resources/parts/gliders");
        }
    }
}