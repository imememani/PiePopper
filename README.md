# PiePopper
 Cracks piepops mods.
 
 # Why?
What he does is immoral and heavily against the license agreement of the B&S SDK, therefore any attempt to take this repo down will be met with a request to have his content taken down, this repo simply removes segments of his code responsible for breaking the license agreement, it does not redistribute any of his code and therefore he can not claim any rights to this repo or its content.

# HOW TO USE
Drag the dll into the .exe!

[Example Video](https://i.imgur.com/tHvhmNk.mp4)

# SUPPORTED MODS
- TrialsoftheShinobi.dll

# HOW DO I MAKE A CRACKER?
New crackers can be made via just:
```cs
[PieHitList("YOUR NAME", "FILE NAME", "FILE VERSION", "CRACK DATE")]]
public class Cracker: Popper
{
        /// <inheritdoc/>
        public override void Bake()
        {
          // Called before Crack for cache/setup
        }

        /// <inheritdoc/>
        public override bool Crack()
        {
          // Actual code ran to crack
        }
}
```
See [TrialsoftheShinobi.cs](https://github.com/imememani/PiePopper/blob/main/Scripts/Crackers/TrialsoftheShinobi.cs) for an example.
Once you have tested and verified it's working create a pull request and I'll merge it.
