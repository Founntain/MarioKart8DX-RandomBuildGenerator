using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SkiaSharp;

namespace Mk8RPBot.Classes{
    public sealed class BuildGenerator{
        private readonly int _defaultHeight = 128;
        private readonly int _defaultCharSize = 128;
        private readonly int _defaultPartSize = 200;

        public MemoryStream Generate(int amount, bool wiiU = false, bool excludeInline = false){
            var stream = new MemoryStream();

            using var surface = SKSurface.Create(new SKImageInfo(_defaultPartSize * 3, (_defaultHeight * 2) * amount));

            for(var y = 0; y < amount; y++){
                var character = GetBitmap(GetRandomCharacter(wiiU, excludeInline));
                var vehicle = GetBitmap(GetRandomVehicle(wiiU, excludeInline));
                var tires = GetBitmap(GetRandomTire(wiiU, excludeInline));
                var glider = GetBitmap(GetRandomGlider(wiiU, excludeInline));

                if(amount > 1)
                {
                    surface.Canvas.DrawText($"Build {y+1}",
                        new SKPoint(_defaultPartSize / 8 + _defaultPartSize, _defaultHeight / 4 + (_defaultHeight * 2) * y),
                        new SKPaint(new SKFont(SKTypeface.Default)));
                }

                var paint = new SKPaint();

                paint.Color = new SKColor(255, 255, 255);
                
                surface.Canvas.DrawRect(0, 0, 100, 100, paint);
                
                surface.Canvas.DrawBitmap(character, 0, 0 + (_defaultCharSize * 2) * y);
                surface.Canvas.DrawBitmap(vehicle, 0, _defaultHeight + ((_defaultHeight * 2) * y));
                surface.Canvas.DrawBitmap(tires, _defaultPartSize, _defaultHeight + ((_defaultHeight * 2) * y));
                surface.Canvas.DrawBitmap(glider, _defaultPartSize * 2, _defaultHeight + ((_defaultHeight * 2) * y));
            }

            using var image = surface.Snapshot();
            using var data = image.Encode(SKEncodedImageFormat.Png, 80);
            
            data.SaveTo(stream);
            
            using (var fs = File.OpenWrite("test.png"))
            {
                // save the data to a stream
                data.SaveTo(fs);
            }

            return stream;
        }

        public MemoryStream GenerateWithNames(IList<string> names, bool wiiU = false, bool excludeInline = false){
            var amount = names.Count;
            var stream = new MemoryStream();

            using var surface = SKSurface.Create(new SKImageInfo(_defaultPartSize * 3, (_defaultHeight * 2) * amount));

            for(var y = 0; y < amount; y++){
                var character = GetBitmap(GetRandomCharacter(wiiU, excludeInline));
                var vehicle = GetBitmap(GetRandomVehicle(wiiU, excludeInline));
                var tires = GetBitmap(GetRandomTire(wiiU, excludeInline));
                var glider = GetBitmap(GetRandomGlider(wiiU, excludeInline));

                if(amount > 1)
                {
                    var name = names.FirstOrDefault() ?? $"Build {y + 1}";
                    
                    surface.Canvas.DrawText(name,
                        new SKPoint(_defaultPartSize / 8 + _defaultPartSize, _defaultHeight / 4 + (_defaultHeight * 2) * y),
                        new SKPaint(new SKFont(SKTypeface.Default)));
                }

                surface.Canvas.DrawBitmap(character, 0, 0 + (_defaultCharSize * 2) * y);
                surface.Canvas.DrawBitmap(vehicle, 0, _defaultHeight + ((_defaultHeight * 2) * y));
                surface.Canvas.DrawBitmap(tires, _defaultPartSize, _defaultHeight + ((_defaultHeight * 2) * y));
                surface.Canvas.DrawBitmap(glider, _defaultPartSize * 2, _defaultHeight + ((_defaultHeight * 2) * y));
            }

            using var image = surface.Snapshot();
            using var data = image.Encode(SKEncodedImageFormat.Png, 80);
            
            data.SaveTo(stream);

            return stream;
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

        private SKBitmap GetBitmap(string path)
        {
            var stream = new SKFileStream(path);
            
            var codec = SKCodec.Create(stream);

            return new SKBitmap(codec.Info);
        }
    }
}