# Project 5
## Implementation / Features you added
Player movement: The player can move by pressing W A S D. I also make the player run by pressing the "left shift".

Game Mechanic: The player can not shoot when they do not get the gun yet. The gun will be instantiated at the center of the maze. So the player have to find and pick the gun first. Then it will show the remaining enemies on the screen. The player have to kill all the enemies to win. After all the enemies are killed, the Exit will be active at the end of the maze (the corner). 

Score: Initially the score for each enemy is 1500. But it will decrease by the time. (formula: current score = initial score - elapsed time)

Interactive objects: I put the gun at the center of the maze. And I made a hightlight yellow sphere to help the player find it. When the player collide with the yellow sphere, the player will pick up the gun and be able to shoot enemies.

Speed boost: I added some blue spheres in the maze, which will help the player gain the speed boost for 10 seconds. Within the time, the player will have double move speed.

In-Game menu: I added a menu in the game. The player can see it by pressing Esc. There are three buttons: Resume, Restart, and Quit. The player can also resume the game by pressing Esc again.

Enemy: The enemies will move randomly when they do not have a target (player). I used the raycast to let enemies be able to "see" the player and chase them. The range is 10. I made four cubes rotate around the enemy which just want to make them look better.

Light: I added two lights on the player: spot light and point light. And in order to make the game darker, I adjust the directional light to be sunset.

## References
Gun Assests: https://pinnache.itch.io/3d-gun-pack-free

## Future Development

## Created by
Franklin Pu
