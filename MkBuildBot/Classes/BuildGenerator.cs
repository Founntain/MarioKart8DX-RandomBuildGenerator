using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MkBuildBot.Classes.Stats;
using SkiaSharp;

namespace MkBuildBot.Classes{
    public sealed class BuildGenerator{
        private readonly int _defaultHeight = 128;
        private readonly int _defaultCharSize = 128;
        private readonly int _defaultPartSize = 200;

        #region Paints

        private SKPaint _textPaint = new SKPaint()
        {
            Color = SKColors.White,
            Style = SKPaintStyle.Fill,
            TextSize = 28,
            FakeBoldText = true,
            IsAntialias = true,
            Typeface = SKTypeface.Default
        };
        
        private SKPaint _biggerTextPaint = new SKPaint()
        {
            Color = SKColors.White,
            Style = SKPaintStyle.Fill,
            TextSize = 36,
            FakeBoldText = true,
            IsAntialias = true,
            Typeface = SKTypeface.Default
        };
        
        private SKPaint _barBackgroundPaint = new SKPaint()
        {
            Color = SKColors.DimGray,
            Style = SKPaintStyle.Fill,
            IsAntialias = true
        };
        
        private SKPaint _groundPaint = new SKPaint()
        {
            Color = SKColors.Orange,
            Style = SKPaintStyle.Fill,
            IsAntialias = true
        };
        
        private SKPaint _waterPaint = new SKPaint()
        {
            Color = SKColors.DeepSkyBlue,
            Style = SKPaintStyle.Fill,
            IsAntialias = true
        };
        
        private SKPaint _airPaint = new SKPaint()
        {
            Color = SKColors.YellowGreen,
            Style = SKPaintStyle.Fill,
            IsAntialias = true
        };
        
        private SKPaint _antiGravityPaint = new SKPaint()
        {
            Color = SKColors.Purple,
            Style = SKPaintStyle.Fill,
            IsAntialias = true
        };

        private SKPaint _otherStatPaint = new SKPaint()
        {
            Color = SKColors.Pink,
            Style = SKPaintStyle.Fill,
            IsAntialias = true
        };

        #endregion
        
        public async Task<MemoryStream> Generate(int amount, bool wiiU = false, bool excludeInline = false)
        {
            return await Task.Run(() =>
            {
                var stream = new MemoryStream();

                var imageStatsOffset = wiiU || amount > 1 ? 0 : 550;

                using var surface = SKSurface.Create(new SKImageInfo(_defaultPartSize * 3, ((_defaultHeight * 2) + imageStatsOffset) * amount, SKColorType.Bgra8888, SKAlphaType.Unpremul));

                if (amount < 4)
                {
                    var bgBitmap = GetBitmap($"resources/style/{(wiiU ? "bg2" : "bg")}.png");
                
                    surface.Canvas.DrawBitmap(bgBitmap, 0, 0);
                }

                for(var y = 0; y < amount; y++)
                {
                    var characterPath = GetRandomCharacter(wiiU, excludeInline);
                    var bodyPath = GetRandomVehicle(wiiU, excludeInline);
                    var tirePath = GetRandomTire(wiiU, excludeInline);
                    var gliderPath = GetRandomGlider(wiiU, excludeInline);
                    
                    var character = GetBitmap(characterPath);
                    var body = GetBitmap(bodyPath);
                    var tire = GetBitmap(tirePath);
                    var glider = GetBitmap(gliderPath);

                    surface.Canvas.DrawBitmap(character, 0, 0 + (_defaultCharSize * 2) * y);
                    surface.Canvas.DrawBitmap(body, 0, _defaultHeight + ((_defaultHeight * 2) * y));
                    surface.Canvas.DrawBitmap(tire, _defaultPartSize, _defaultHeight + ((_defaultHeight * 2) * y));
                    surface.Canvas.DrawBitmap(glider, _defaultPartSize * 2, _defaultHeight + ((_defaultHeight * 2) * y));
                    
                    if(amount > 1)
                    {
                        surface.Canvas.DrawText($"Build {y+1}", _defaultPartSize / 8 + _defaultPartSize, (_defaultHeight / 4 + (_defaultHeight * 2) * y) + 30,  _biggerTextPaint );
                        
                        if(amount != y + 1)
                            surface.Canvas.DrawLine(0, _defaultHeight * 2 + ((_defaultHeight * 2) * y), _defaultPartSize * 3, _defaultHeight * 2 + ((_defaultHeight * 2) * y), _textPaint);
                    }

                    #region Render Build Stats

                    if(wiiU || amount > 1) continue;
                    
                    var statGen = new BuildStatGenerator();
                    
                    var build = statGen.GetBuild(characterPath, bodyPath, tirePath, gliderPath);
                    
                    var groundSpeedPercentage = build.GroundSpeed() / 6;
                    var waterSpeedPercentage = build.WaterSpeed() / 6;
                    var airSpeedPercentage = build.GlidingSpeed() / 6;
                    var antiGravitySpeedPercentage = build.AntiGravitySpeed() / 6;

                    var accelerationPercentage = build.Acceleration() / 6;
                    var weightPercentage = build.Weight() / 6;
                    
                    var groundHandlingPercentage = build.GroundHandling() / 6;
                    var waterHandlingPercentage = build.WaterHandling() / 6;
                    var airHandlingPercentage = build.GlidingHandling() / 6;
                    var antiGravityHandlingPercentage = build.AntiGravityHandling() / 6;

                    var tractionPercentage = build.Traction() / 6;
                    var miniTurboPercentage = build.MiniTurbo() / 6;
                    var invincibilityPercentage = build.Invincibility() / 6;
                    
                    var statsOffset = 275;

                    var barBitmap = GetBitmap("resources/style/bar.png");

                    #region Speed

                    surface.Canvas.DrawText("Speed (Ground, Water, Air, AntiGravity)", 5, 35 + statsOffset,  _textPaint );

                    surface.Canvas.DrawRoundRect(new SKRect(0, 50 + statsOffset, _defaultPartSize * 3, 60 + statsOffset), 5, 5, _barBackgroundPaint);
                    surface.Canvas.DrawRoundRect(new SKRect(0, 63 + statsOffset, _defaultPartSize * 3, 73 + statsOffset), 5, 5, _barBackgroundPaint);
                    surface.Canvas.DrawRoundRect(new SKRect(0, 76 + statsOffset, _defaultPartSize * 3, 86 + statsOffset), 5, 5, _barBackgroundPaint);
                    surface.Canvas.DrawRoundRect(new SKRect(0, 89 + statsOffset, _defaultPartSize * 3, 99 + statsOffset), 5, 5, _barBackgroundPaint);

                    surface.Canvas.DrawRoundRect(new SKRect(0, 50 + statsOffset, (float) (_defaultPartSize * 3 * groundSpeedPercentage), 60 + statsOffset), 5, 5, _groundPaint);
                    surface.Canvas.DrawRoundRect(new SKRect(0, 63 + statsOffset, (float) (_defaultPartSize * 3 * waterSpeedPercentage), 73 + statsOffset), 5, 5, _waterPaint);
                    surface.Canvas.DrawRoundRect(new SKRect(0, 76 + statsOffset, (float) (_defaultPartSize * 3 * airSpeedPercentage), 86 + statsOffset), 5, 5, _airPaint);
                    surface.Canvas.DrawRoundRect(new SKRect(0, 89 + statsOffset, (float) (_defaultPartSize * 3 * antiGravitySpeedPercentage), 99 + statsOffset), 5, 5, _antiGravityPaint);
                    
                    surface.Canvas.DrawBitmap(barBitmap, 0, 50 + statsOffset);
                    surface.Canvas.DrawBitmap(barBitmap, 0, 63 + statsOffset);
                    surface.Canvas.DrawBitmap(barBitmap, 0, 76 + statsOffset);
                    surface.Canvas.DrawBitmap(barBitmap, 0, 89 + statsOffset);

                    #endregion

                    #region Acceleration

                    surface.Canvas.DrawText("Acceleration", 5, 135 + statsOffset,  _textPaint );
                    
                    surface.Canvas.DrawRoundRect(new SKRect(0, 150 + statsOffset, _defaultPartSize * 3, 160 + statsOffset), 5, 5, _barBackgroundPaint);
                    surface.Canvas.DrawRoundRect(new SKRect(0, 150 + statsOffset, (float) (_defaultPartSize * 3 * accelerationPercentage), 160 + statsOffset), 5, 5, _otherStatPaint);
                    
                    surface.Canvas.DrawBitmap(barBitmap, 0, 150 + statsOffset);

                    #endregion

                    #region Weight

                    surface.Canvas.DrawText("Weight", 5, 195 + statsOffset,  _textPaint );
                    
                    surface.Canvas.DrawRoundRect(new SKRect(0, 210 + statsOffset, _defaultPartSize * 3, 220 + statsOffset), 5, 5, _barBackgroundPaint);
                    surface.Canvas.DrawRoundRect(new SKRect(0, 210 + statsOffset, (float) (_defaultPartSize * 3 * weightPercentage), 220 + statsOffset), 5, 5, _otherStatPaint);
                    
                    surface.Canvas.DrawBitmap(barBitmap, 0, 210 + statsOffset);

                    #endregion

                    #region Handling

                    surface.Canvas.DrawText("Handling (Ground, Water, Air, AntiGravity)", 5, 255 + statsOffset,  _textPaint );
                    
                    surface.Canvas.DrawRoundRect(new SKRect(0, 270 + statsOffset, _defaultPartSize * 3, 280 + statsOffset), 5, 5, _barBackgroundPaint);
                    surface.Canvas.DrawRoundRect(new SKRect(0, 283 + statsOffset, _defaultPartSize * 3, 293 + statsOffset), 5, 5, _barBackgroundPaint);
                    surface.Canvas.DrawRoundRect(new SKRect(0, 296 + statsOffset, _defaultPartSize * 3, 306 + statsOffset), 5, 5, _barBackgroundPaint);
                    surface.Canvas.DrawRoundRect(new SKRect(0, 309 + statsOffset, _defaultPartSize * 3, 319 + statsOffset), 5, 5, _barBackgroundPaint);
                    
                    surface.Canvas.DrawRoundRect(new SKRect(0, 270 + statsOffset, (float) (_defaultPartSize * 3 * groundHandlingPercentage), 280 + statsOffset), 5, 5, _groundPaint);
                    surface.Canvas.DrawRoundRect(new SKRect(0, 283 + statsOffset, (float) (_defaultPartSize * 3 * waterHandlingPercentage), 293 + statsOffset), 5, 5, _waterPaint);
                    surface.Canvas.DrawRoundRect(new SKRect(0, 296 + statsOffset, (float) (_defaultPartSize * 3 * airHandlingPercentage), 306 + statsOffset), 5, 5, _airPaint);
                    surface.Canvas.DrawRoundRect(new SKRect(0, 309 + statsOffset, (float) (_defaultPartSize * 3 * antiGravityHandlingPercentage), 319 + statsOffset), 5, 5, _antiGravityPaint);
                    
                    surface.Canvas.DrawBitmap(barBitmap, 0, 270 + statsOffset);
                    surface.Canvas.DrawBitmap(barBitmap, 0, 283 + statsOffset);
                    surface.Canvas.DrawBitmap(barBitmap, 0, 296 + statsOffset);
                    surface.Canvas.DrawBitmap(barBitmap, 0, 309 + statsOffset);

                    #endregion

                    #region Traction

                    surface.Canvas.DrawText("Traction", 5, 355 + statsOffset,  _textPaint );
                    
                    surface.Canvas.DrawRoundRect(new SKRect(0, 370 + statsOffset, _defaultPartSize * 3, 380 + statsOffset + (355 + statsOffset) * y), 5, 5, _barBackgroundPaint);
                    surface.Canvas.DrawRoundRect(new SKRect(0, 370 + statsOffset, (float) (_defaultPartSize * 3 * tractionPercentage), 380 + statsOffset), 5, 5, _otherStatPaint);
                    
                    surface.Canvas.DrawBitmap(barBitmap, 0, 370 + statsOffset);

                    #endregion

                    #region Mini-Turbo

                    surface.Canvas.DrawText("Mini-Turbo", 5, 415 + statsOffset,  _textPaint );
                    
                    surface.Canvas.DrawRoundRect(new SKRect(0, 430 + statsOffset, _defaultPartSize * 3, 440 + statsOffset), 5, 5, _barBackgroundPaint);
                    surface.Canvas.DrawRoundRect(new SKRect(0, 430 + statsOffset, (float) (_defaultPartSize * 3 * miniTurboPercentage), 440 + statsOffset), 5, 5, _otherStatPaint);
                    
                    surface.Canvas.DrawBitmap(barBitmap, 0, 430 + statsOffset);

                    #endregion

                    #region Invincibility

                    surface.Canvas.DrawText("Invincibility", 5, 475 + statsOffset,  _textPaint );
                    
                    surface.Canvas.DrawRoundRect(new SKRect(0, 490 + statsOffset, _defaultPartSize * 3, 500 + statsOffset), 5, 5, _barBackgroundPaint);
                    surface.Canvas.DrawRoundRect(new SKRect(0, 490 + statsOffset, (float) (_defaultPartSize * 3 * invincibilityPercentage), 500 + statsOffset), 5, 5, _otherStatPaint);
                    
                    surface.Canvas.DrawBitmap(barBitmap, 0, 490 + statsOffset);

                    #endregion

                    #endregion

                    surface.Canvas.Flush();
                }

                using var data = surface.Snapshot().Encode(SKEncodedImageFormat.Png, 80);
            
                data.SaveTo(stream);

                return stream;
            });
        }

        public async Task<MemoryStream> GenerateWithNames(IList<string> names, bool wiiU = false, bool excludeInline = false)
        {
            return await Task.Run(() =>
            {
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
                "birdo.png",
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

            return SKBitmap.Decode(stream);
        }
    }
}