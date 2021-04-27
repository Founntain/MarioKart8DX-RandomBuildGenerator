using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace mk8bot.Commands{
    public sealed class BuildGenerator{
        private readonly int DefaultHeight = 128;
        private readonly int CharDefaultSize = 128;
        private readonly int PartDefaultSize = 200;

        public MemoryStream Generate(){
            var stream = new MemoryStream();

            var character = Image.FromFile(GetRandomCharacter());
            var vehicle = Image.FromFile(GetRandomVehicle());
            var tires = Image.FromFile(GetRandomTire());
            var glider = Image.FromFile(GetRandomGlider());

            using(var bitmap = new Bitmap(CharDefaultSize + PartDefaultSize * 3, DefaultHeight * 2)){
                using(var g = Graphics.FromImage(bitmap)){
                    g.DrawImage(character, 0, 0);
                    g.DrawImage(vehicle, 0, DefaultHeight);
                    g.DrawImage(tires, PartDefaultSize, DefaultHeight);
                    g.DrawImage(glider, PartDefaultSize * 2, DefaultHeight);
                }

                bitmap.Save(stream, ImageFormat.Png);
                
                return stream;
            };
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