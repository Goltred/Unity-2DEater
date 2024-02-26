# Tiny Muncher! information

## Assumptions:
* For ease of setup, the play area is the camera viewport. This allows for easier modification of the 
boundaries as just the FOV of the camera needs to be changed.
* The game is intended for desktop and has not been tested on other devices
* Performance was mentioned, so:
  * I avoided to use the physics system as much as possible
  * I tried to limit game object instantiation

## Edge cases:
* I assumed most people would try to use WSAD for movement, so I also implemented those keys for movement.
* No controller support was added
* Resolution handling is not the best. It works well for standard resolutions but no so much for ultra wide screens

## Future ideas:
* More levels, different backgrounds
* Create a story: I always had in mind of making this a two sided story for "Roach", he's eating food right now, but a specific type of food 
would trigger his "true" side where he's eating escaping souls in hellish devastated scenes
* Skills: I envisioned that the character could jump around or dodge or slow down time depending on some sort of progression/level system
* Enemies and "bad" foods: Add some enemies to the mix, enemies come in and try to ram the player or shoot at him. Getting hit means 
losing points or having a health system in place that would cause the level to be lost. Bad foods would work the same way, forcing the player
to have to discern between what to eat and not\
* Progression: Think candy crash. A map where the player is able to choose what to play next, the selected scenario follows the story and
presents the players with different food types, enemies, obstacles, etc.
* Better graphics... Yep, better graphics :)