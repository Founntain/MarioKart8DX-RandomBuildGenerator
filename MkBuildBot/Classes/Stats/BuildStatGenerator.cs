using System.Collections.Generic;
using System.IO;

namespace MkBuildBot.Classes.Stats;

public class BuildStatGenerator
{
    private List<PartStat> Characters => GetCharacterBases();

    private List<PartStat> Bodies => GetBodyBases();

    private List<PartStat> Tires => GetTireBases();

    private List<PartStat> Gliders => GetGliderBases();

    public Build GetBuild(string character, string body, string tire, string glider)
    {
        var characterPart = GetCharacter(Path.GetFileNameWithoutExtension(character));
        var bodyPart = GetBody(Path.GetFileNameWithoutExtension(body));
        var tirePart = GetTire(Path.GetFileNameWithoutExtension(tire));
        var gliderPart = GetGlider(Path.GetFileNameWithoutExtension(glider));

        return new Build(characterPart, bodyPart, tirePart, gliderPart);
    }

    #region Get Part Methods

    private PartStat GetCharacter(string character)
    {
        switch (character)
        {
            case "babydaisy":
            case "babypeach":
                return Characters[0];
            case "babyrosalina":
            case "lemmy":
                return Characters[1];
            case "babymario":
            case "babyluigi":
            case "drybones":
            case "lightmii":
                return Characters[2];
            case "toadette":
            case "wendy":
            case "isabelle":
                return Characters[3];
            case "koopa":
            case "lakitu":
            case "bowserjr":
                return Characters[4];
            case "toad":
            case "shyguy":
            case "larry":
                return Characters[5];
            case "catpeach":
            case "inklingfemale":
            case "villagerfemale":
            case "diddykong":
                return Characters[6];
            case "peach":
            case "daisy":
            case "yoshi":
            case "birdo":
            case "peachette":
                return Characters[7];
            case "tanookimario":
            case "inklingmale":
            case "villagermale":
                return Characters[8];
            case "mario":
            case "ludwig":
            case "mediummii":
                return Characters[9];
            case "luigi":
            case "iggy":
            case "kamek":
                return Characters[10];
            case "rosalina":
            case "kingboo":
            case "link":
            case "pauline":
                return Characters[11];
            case "metalmario":
            case "goldmario":
            case "rosegoldpeach":
                return Characters[12];
            case "waluigi":
            case "dk":
            case "roy":
            case "wiggler":
                return Characters[13];
            case "wario":
            case "drybowser":
            case "heavymii":
            case "funkykong":
                return Characters[14];
            case "bowser":
            case "morton":
                return Characters[15];
            case "petey":
                return Characters[16];
            default: return new PartStat();
        }

        ;
    }

    private PartStat GetBody(string body)
    {
        switch (body)
        {
            case "standard":
            case "theduke":
                return Bodies[0];
            case "300slroadster":
                return Bodies[1];
            case "pipeframe":
                return Bodies[2];
            case "varmint":
            case "citytripper":
                return Bodies[3];
            case "mach8":
            case "sportscoupe":
                return Bodies[4];
            case "inkstriker":
                return Bodies[5];
            case "steeldriver":
            case "trispeeder":
                return Bodies[6];
            case "bonerattler":
                return Bodies[7];
            case "catcruiser":
            case "teddybuggy":
                return Bodies[8];
            case "comet":
            case "yoshibike":
                return Bodies[9];
            case "circuitspecial":
            case "mk8dasher":
            case "pwing":
                return Bodies[10];
            case "badwagon":
            case "gla":
                return Bodies[11];
            case "standardatv":
                return Bodies[12];
            case "prancerbody":
                return Bodies[13];
            case "sportbike":
            case "jetbike":
                return Bodies[14];
            case "biddybuggy":
            case "mrscooty":
                return Bodies[15];
            case "landship":
                return Bodies[16];
            case "streetle":
                return Bodies[17];
            case "sneaker":
                return Bodies[18];
            case "goldstandard":
                return Bodies[19];
            case "mastercycle":
                return Bodies[20];
            case "w25silverarrow":
                return Bodies[21];
            case "standardbike":
            case "flamerider":
                return Bodies[22];
            case "wildwiggler":
                return Bodies[23];
            case "bluefalcon":
                return Bodies[24];
            case "splatbuggy":
                return Bodies[25];
            case "tanookibuggy":
                return Bodies[26];
            case "koopaclown":
            case "mastercyclezero":
                return Bodies[27];
            default: return new PartStat();
        }
    }

    private PartStat GetTire(string tire)
    {
        switch (tire)
        {
            case "standard":
            case "standardblue":
                return Tires[0];
            case "gla":
                return Tires[1];
            case "monster":
            case "monsterorange":
                return Tires[2];
            case "ancient":
                return Tires[3];
            case "roller":
            case "rollerazure":
                return Tires[4];
            case "slim":
            case "slimcrimson":
            case "wood":
                return Tires[5];
            case "slick":
            case "slickcyber":
                return Tires[6];
            case "metal":
                return Tires[7];
            case "gold":
                return Tires[8];
            case "button":
            case "leaf":
                return Tires[9];
            case "offroad":
            case "offroadretro":
                return Tires[10];
            case "triforce":
                return Tires[11];
            case "sponge":
                return Tires[12];
            case "cushion":
                return Tires[13];
            default: return new PartStat();
        }
    }

    private PartStat GetGlider(string glider)
    {
        switch (glider)
        {
            case "standard":
            case "waddle":
            case "hylian":
                return Gliders[0];
            case "cloud":
            case "parachute":
            case "flower":
            case "paper":
                return Gliders[1];
            case "wario":
            case "plane":
            case "gold":
            case "paraglider":
                return Gliders[2];
            case "peach":
            case "parafoil":
            case "bowser":
            case "mktv":
                return Gliders[3];
            default: return new PartStat();
        }
    }

    #endregion

    #region Base Stats

    private static List<PartStat> GetCharacterBases()
    {
        return new List<PartStat>
        {
            new("LightChar1", 2.5, 2.25, 2.75, 3, 4, 2, 5, 5, 4.5, 5, 4.25, 4.5, 4.25), // Baby Peach, Baby Daisy
            new("LightChar2", 2.5, 2.25, 2.75, 3, 4.25, 2, 4.75, 4.75, 4.25, 4.75, 3.75, 4.5, 4.25), // Baby Rosalina, Lemmi
            new("LightChar3", 2.75, 2.5, 3, 3.25, 4.25, 2.25, 4.5, 4.5, 4, 4.5, 4, 4.5, 4), // Baby Mario, Baby Luigi, Dry Bones, Light Mii
            new("LightChar4", 3, 2.75, 3.25, 3.5, 4.25, 2.5, 4.25, 4.25, 3.75, 4.25, 3.5, 4.25, 3.5), // Toadette, Wendy, Isabelle
            new("LightChar5", 3, 2.75, 3.25, 3.5, 4, 2.5, 4.5, 4.5, 4, 4.5, 4.25, 4.25, 3.75), // Koopa Troopa, Lakitu, Bowser Jr.
            new("LightChar6", 3.25, 3, 3.5, 3.75, 4, 2.75, 4.25, 4.25, 3.75, 4.25, 4, 4.25, 3.5), // Toad, Shy Guy, Larry
            new("MediumChar1", 3.5, 3.25, 3.75, 4, 4, 2.75, 4, 4, 3.5, 4, 3.75, 4.25, 3.5), // Cat Peach, Inkling Girl, Female Villager, Diddy Kong
            new("MediumChar2", 3.75, 3.5, 4, 4.25, 3.75, 3, 3.75, 3.75, 3.25, 3.75, 3.75, 4.25, 3), // Peach, Daisy, Yoshi, Birdo, Peachette
            new("MediumChar3", 3.75, 3.5, 4, 4.25, 3.75, 3.25, 3.75, 3.75, 3.25, 3.75, 3.25, 4.25, 3), // Tanuki Mario, Inkling Boy, Male Villager
            new("MediumChar4", 4, 3.75, 4.25, 4.5, 3.5, 3.5, 3.5, 3.5, 3, 3.5, 3.5, 4, 3.5), // Mario, Ludwig, Medium Mii
            new("MediumChar5", 4, 3.75, 4.25, 4.5, 3.5, 3.5, 3.75, 3.75, 3.25, 3.75, 3.25, 4, 3.5), // Luigi, Iggy, Kamek
            new("HeavyChar1", 4.25, 4, 4.5, 4.75, 3.25, 3.75, 3.25, 3.25, 2.75, 3.25, 3.75, 3.75, 3.75), // Rosalina, King Boo, Link, Pauline
            new("HeavyChar2", 4.25, 4, 4.5, 4.75, 3.25, 4.5, 3.25, 3.25, 2.75, 3.25, 3.25, 3.5, 3.5), // Metal Mario, Gold Mario, Pink Gold Peach
            new("HeavyChar3", 4.5, 4.25, 4.75, 5, 3.25, 4, 3, 3, 2.5, 3, 3, 3.5, 3.75), // Waluigi, DK, Roy, Wiggler
            new("HeavyChar4", 4.75, 4.5, 5, 5.25, 3, 4.25, 2.75, 2.75, 2.25, 2.75, 3.25, 3.25, 4), // Wario, Dry Bowser, Heavy Mii, Funky Kong
            new("HeavyChar5", 4.75, 4.5, 5, 5.25, 3, 4.5, 2.5, 2.5, 2, 2.5, 3, 3.25, 4.25), // Bowser, Morton
            new("HeavyChar6", 4.25, 4, 4.5, 4.75, 3.25, 4.5, 3.25, 3.25, 2.75, 3.25, 3.25, 3.5, 4.25) // Petey Piranha
        };
    }

    private List<PartStat> GetBodyBases()
    {
        return new List<PartStat>
        {
            new("Body1", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0), // Standard Kart & Duke
            new("Body2", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0.25), // 300 SL Roadster

            new("Body3", -0.25, -0.5, 0, -0.5, 0.5, -0.25, 0.5, 0.25, 0.5, -0.25, 0.25, 0.25, -0.25), // Pipe Frame
            new("Body4", -0.25, -0.5, 0, -0.5, 0.5, -0.25, 0.5, 0.25, 0.5, -0.25, 0.25, 0.25, -0.5), // Varmint & City Tripper

            new("Body5", 0, 0.5, 0, 0.25, -0.25, 0.25, -0.25, 0.25, 0, -0.25, 0.25, 0, 0.25), // Mach 8 & Sports coupe
            new("Body6", 0, 0.5, 0, 0.25, -0.25, 0.25, -0.25, 0.25, 0, -0.25, 0.25, 0, 0), // Inkstriker

            new("Body7", 0.25, -0.25, 0.5, -0.75, -0.75, 0.5, -0.5, -0.5, 0.75, -0.5, 0, -0.5, 0.75), // Steel Driver & Tri Speeder
            new("Body8", 0.25, -0.25, 0.5, -0.75, -0.75, 0.5, -0.5, -0.5, 0.75, -0.5, 0, -0.5, 0.5), // Bone Rattler

            new("Body9", -0.25, 0, -0.25, 0.25, 0.25, 0, 0.25, 0, 0, 0.25, 0, 0.25, 0), // Cat Cruiser & Teddy Buggy
            new("Body10", -0.25, 0, -0.25, 0.25, 0.25, 0, 0.25, 0, 0, 0.25, 0, 0.25, -0.25), // Comet & Yoshi Bike

            new("Body11", 0.5, 0.25, -0.5, -0.25, -0.75, 0.25, -0.5, -0.25, -0.25, -0.75, -0.5, -0.5, 0.75), // Circuit Special, B-Dasher, P-Wing

            new("Body12", 0.5, 0, -0.25, -0.5, -1, 0.5, -0.75, -0.5, -0.25, -0.75, 0.5, -0.5, 1), // Badwagon, GLA
            new("Body13", 0.5, 0, -0.25, -0.5, -1, 0.5, -0.75, -0.5, -0.25, -0.75, 0.5, -0.5, 0.75), // Standard ATV

            new("Body14", 0.25, 0, 0, 0, -0.5, -0.25, 0, -0.25, 0.25, 0, -0.25, -0.25, 0.5), // Prancer
            new("Body15", 0.25, 0, 0, 0, -0.5, -0.25, 0, -0.25, 0.25, 0, -0.25, -0.25, 0), // Sport Bike Jet Bike

            new("Body16", -0.75, -0.25, -0.5, -0.5, 0.75, -0.5, 0.5, 0.5, 0.5, 0.25, 0.25, 0.5, -0.75), // Biddybuggy & Mister Scooty

            new("Body17", -0.25, -0.75, 0.5, -0.25, 0.5, -0.5, 0.25, -0.25, 0.75, 0, 0.75, 0.25, -0.5), // Landship
            new("Body18", -0.25, -0.75, 0.5, -0.25, 0.5, -0.5, 0.25, -0.25, 0.75, 0, 0.75, 0.25, -0.25), // Streetle

            new("Body19", 0.25, 0, -0.25, 0, -0.5, 0, 0, 0, 0, -0.25, -0.75, -0.25, 0.5), // Sneeker
            new("Body20", 0.25, 0, -0.25, 0, -0.5, 0, 0, 0, 0, -0.25, -0.75, -0.25, 0.25), // Gold Standard
            new("Body21", 0.25, 0, -0.25, 0, -0.5, 0, 0, 0, 0, -0.25, -0.75, -0.25, 0), // Master Cycle

            new("Body22", -0.25, 0.25, -0.25, 0, 0.25, -0.25, 0.25, 0.25, 0.25, 0, 0.5, 0, 0), // W 25 Silver Arrow
            new("Body23", -0.25, 0.25, -0.25, 0, 0.25, -0.25, 0.25, 0.25, 0.25, 0, 0.5, 0, -0.25), // Standard Bike & Flame Rider
            new("Body24", -0.25, 0.25, -0.25, 0, 0.25, -0.25, 0.25, 0.25, 0.25, 0, 0.5, 0, -0.5), // Wild Wiggler

            new("Body25", 0.25, 0.25, -0.25, 0, -0.25, -0.5, -0.25, 0.5, 0.25, -0.5, 0, -0.25, 0.25), // Blue Falcon
            new("Body26", 0.25, 0.25, -0.25, 0, -0.25, -0.5, -0.25, 0.5, 0.25, -0.5, 0, -0.25, 0), // Splat Buggy

            new("Body27", 0, 0, 0.25, 0, -0.5, 0.25, 0.25, 0, 0.5, 0, 1, 0, 0.25), // Tanooki Kart
            new("Body28", 0, 0, 0.25, 0, -0.5, 0.25, 0.25, 0, 0.5, 0, 1, 0, 0) // Koopa Clown & Master Cycle Zero
        };
    }

    private List<PartStat> GetTireBases()
    {
        return new List<PartStat>
        {
            new("Tire1", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0), // Standard & Blue
            new("Tire2", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0.25), // GLA

            new("Tire3", 0.25, 0, -0.25, -0.5, -0.5, 0.5, -0.75, -0.75, -0.5, -0.5, 0.5, -0.25, 0.5), // Monster & Hot Monster
            new("Tire4", 0.25, 0, -0.25, -0.5, -0.5, 0.5, -0.75, -0.75, -0.5, -0.5, 0.5, -0.25, 0.25), // Ancient

            new("Tire5", -0.5, -0.5, 0, 0, 0.5, -0.5, 0.25, 0.25, 0.25, 0.25, -0.25, 0.5, -0.75), // Roller & Azure Roller

            new("Tire6", 0.25, 0.5, -0.25, -0.25, -0.5, 0, 0.25, 0, 0.25, 0.25, -1, -0.25, 0.25), // Slim & Wood, Crimson Slim

            new("Tire7", 0.5, 0.5, -0.75, -0.75, -0.75, 0.25, -0.25, -0.25, -0.75, -0.5, -1.25, -0.75, 0.25), // Slick &  Cyber Slick

            new("Tire8", 0.5, -0.25, 0, -0.25, -1, 0.5, -0.25, -0.5, -0.25, -0.75, -0.75, -0.50, 0.5), // Metal
            new("Tire9", 0.5, -0.25, 0, -0.25, -1, 0.5, -0.25, -0.5, -0.25, -0.75, -0.75, -0.50, 0.25), // Gold

            new("Tire10", -0.25, 0, -0.25, -0.25, 0.25, -0.5, 0, 0.25, 0, -0.25, -0.5, 0.25, -0.25), // Button & Leaf

            new("Tire11", 0.25, 0, 0.25, -0.5, -0.25, 0.25, -0.5, -0.25, -0.5, -0.25, 0.25, -0.25, 0.5), // Offroad & Retro Offroad
            new("Tire12", 0.25, 0, 0.25, -0.5, -0.25, 0.25, -0.5, -0.25, -0.5, -0.5, 0.25, -0.5, 0.5), // TriForce

            new("Tire13", -0.25, -0.25, -0.5, 0.25, 0, -0.25, -0.25, -0.25, -0.5, 0, 0.25, 0.25, 0), // Sponge
            new("Tire14", -0.25, -0.25, -0.5, 0.25, 0, -0.25, -0.25, -0.25, -0.5, 0, 0.25, 0.25, 0.5) // Cushion
        };
    }

    private static List<PartStat> GetGliderBases()
    {
        return new List<PartStat>
        {
            new("Glider1", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0), // Super, Woddle Wing, Hylian Kite
            new("Glider2", -0.25, 0.25, 0, -0.25, 0.25, -0.25, 0, 0, 0, 0.25, 0, 0.25, -0.25), // Cloud, Parachute, Flower & Paper Glider
            new("Glider3", 0, 0.25, -0.25, 0, 0, 0.25, 0, -0.25, 0.25, 0, -0.25, 0, 0), // Wario, Plane, Gold, Paraglider
            new("Glider4", -0.25, 0.25, -0.25, -0.25, 0.25, 0, 0, -0.25, 0.25, 0.25, -0.25, 0.25, -0.25) // Peach, Parafoil, bowser kite, MKTV
        };
    }

    #endregion
}