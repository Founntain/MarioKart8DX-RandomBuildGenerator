using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace mk8bot.Classes{
    public sealed class BuildGenerator{
        private readonly int DefaultHeight = 128;
        private readonly int DefaultCharSize = 128;
        private readonly int DefaultPartSize = 200;

        public async Task<MemoryStream> Generate(bool wiiU = false, bool excludeInline = false){
            var stream = new MemoryStream();

            var character = Image.FromFile(GetRandomCharacter(wiiU, excludeInline));
            var vehicle = Image.FromFile(GetRandomVehicle(wiiU, excludeInline));
            var tires = Image.FromFile(GetRandomTire(wiiU, excludeInline));
            var glider = Image.FromFile(GetRandomGlider(wiiU, excludeInline));

            using(var bitmap = new Bitmap(DefaultPartSize * 3, DefaultHeight * 2)){
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

        public async Task<MemoryStream> Generate(int amount, bool wiiU = false, bool excludeInline = false){
            return await Task.Run(() => {
                var stream = new MemoryStream();

                using(var bitmap = new Bitmap(DefaultPartSize * 3, (DefaultHeight * 2) * amount)){
                    using(var g = Graphics.FromImage(bitmap)){
                        for(var y = 0; y < amount; y++){

                            var character = Image.FromFile(GetRandomCharacter(wiiU, excludeInline));
                            var vehicle = Image.FromFile(GetRandomVehicle(wiiU, excludeInline));
                            var tires = Image.FromFile(GetRandomTire(wiiU, excludeInline));
                            var glider = Image.FromFile(GetRandomGlider(wiiU, excludeInline));

                            if(amount > 1)
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

        public async Task<MemoryStream> GenerateWithNames(IList<string> names, bool wiiU = false, bool excludeInline = false){
            return await Task.Run(() => {
                var amount = names.Count;
                var stream = new MemoryStream();

                using(var bitmap = new Bitmap(DefaultPartSize * 3, (DefaultHeight * 2) * amount)){
                    using(var g = Graphics.FromImage(bitmap)){
                        for(var y = 0; y < amount; y++){

                            var character = Image.FromFile(GetRandomCharacter(wiiU, excludeInline));
                            var vehicle = Image.FromFile(GetRandomVehicle(wiiU, excludeInline));
                            var tires = Image.FromFile(GetRandomTire(wiiU, excludeInline));
                            var glider = Image.FromFile(GetRandomGlider(wiiU, excludeInline));

                            g.DrawString(names.FirstOrDefault() ?? $"Build {y+1}", new Font("Arial", 35f, FontStyle.Bold), Brushes.White, new PointF(DefaultPartSize / 8 + DefaultPartSize, DefaultHeight / 4 + (DefaultHeight * 2) * y));

                            g.DrawImage(character, 0, 0 + (DefaultCharSize * 2) * y);
                            g.DrawImage(vehicle, 0, DefaultHeight + ((DefaultHeight * 2) * y));
                            g.DrawImage(tires, DefaultPartSize, DefaultHeight + ((DefaultHeight * 2) * y));
                            g.DrawImage(glider, DefaultPartSize * 2, DefaultHeight + ((DefaultHeight * 2) * y));

                            names.RemoveAt(0);
                        }   
                    }

                    bitmap.Save(stream, ImageFormat.Png);
                    
                    return stream;
                };
            });
        }

        private string GetRandomImageFromDirectory(string dir, bool wiiU, bool excludeInline){
            var files = Directory.GetFiles(dir);
            var random = new Random();

            while(true){
                var value = random.Next(0, files.Length - 1);
                var part = files[value];

                if(!wiiU && !excludeInline)
                    return part;

                var pathToCheck = Path.GetFileName(part);

                if(wiiU)
                    if(GetDeluxeOnlyParts().Any(x => x.ToLower() == pathToCheck)) continue;
                
                if(excludeInline)
                    if(GetInlineBikes().Any(x => x.ToLower() == pathToCheck)) continue;
                
                return part;
            }
        }

        private string GetRandomCharacter(bool wiiU = false, bool excludeInline = false){
            return GetRandomImageFromDirectory("resources/chars", wiiU, excludeInline);
        }

        private string GetRandomVehicle(bool wiiU = false, bool excludeInline = false){
            return GetRandomImageFromDirectory("resources/parts/vehicles", wiiU, excludeInline);
        }

        private string GetRandomTire(bool wiiU = false, bool excludeInline = false){
            return GetRandomImageFromDirectory("resources/parts/tires", wiiU, excludeInline);
        }

        private string GetRandomGlider(bool wiiU = false, bool excludeInline = false){
            return GetRandomImageFromDirectory("resources/parts/gliders", wiiU, excludeInline);
        }

        private ICollection<string> GetDeluxeOnlyParts(){
            return new List<string>(){
                //vehicles
                "koopaclown.png",
                "mastercyclezero.png",
                "splatbuggy.png",
                "inkstriker.png",
                //tires
                "ancient.png",
                //gliders
                "paraglider.png",
                //characters
                "kingboo.png",
                "goldmario.png",
                "drybones.png",
                "bowserjr.png",
                "inklingfemale.png",
                "inklingmale.png"
            };
        }

        private ICollection<string> GetInlineBikes(){
            return new List<string>(){
                "comet.png",
                "sportbike.png",
                "jetbike.png",
                "yoshibike.png",
                "mastercycle.png"
            };
        }
    }
}