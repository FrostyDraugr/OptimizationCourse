# OptimizationCourse
  ## Dependencies:
        Made with Unity; version 2022.3.10f1

  ## TODO Simple Space Game with the Following Mechanics:
        1. Simple Movement
        2. Shooting
        3. Waves of Enemies

  ## Pre-Dots:
  1. - [x]
  2. - [x]
  3. - [x]
      ### Controls:
         W - Forward Acceleration
         Mouse - Rotation
         Mouse Left Click - Fire
        
     ### Pre-DOTS Summary
         For the Pre-DOTS version I decided to use RigidBodies for collision and movement on the; Player, the Bullet, and the "Asteroids".
         The game was made to be as simple as possible so I could move on to the main assigment using DOTS.
         The initial tests showed a massive drop in FPS during in-engine play, running in at complete stops when the game would go over 5k Game Objects in the scene.
         In the actual build of the Pre-DOTS version, the game runs on 60fps up until the 5k enemy mark is hit.
         The game then can make it to 6k where it stops running almost completetly.

         Something worth of note is the fact that the Pre-DOTS version doesn't utilize my computer's hardware at all.
         CPU Utilization hits 11% in the build, even with 8k GameObjects and 1FPS.
         The profiler also confirmed that my biggest bottleneck were CPU related, so the real goal for me when it came to DOTS was to try and offload as much traffic
         from my single core. Either through multithreading our trough utilizing my GPU.
         And though the GPU does see some use, the real bottleneck here is the CPU calculations for the physics.
         
 
 ### Post-DOTS:
  1. - [x]
  2. - [x]
  3. - [x]
  ## Controls:
         WASD - Movement
         Mouse Left Click - Fire
         Q - Quit (I don't know why I didn't implement this in the Pre-Dots version..)
 
  ### Post-DOTS Summary 1 (See TAG: DOTS-withoutJobs.v1)
        Foreword: I've never worked with DOTS in my life before and it proved to be quite the challenge.
        My initial outset to creating the DOTS version was to try and implement as few calculations and loops as I possibly could,
        trying to never have too much traffic simultaneously.

        The mainECS subscene contains a pre-loaded GameManager with two different Authoring Scripts attached to them, one for controlling the
        game statistics, and one for controlling what ISystems are allowed to Update or not.

        This was especially handy during development, allowing me to test specific systems and making sure Enteties existed in the
        scene using IComponent tags to make sure I never returned a null ref.

        For my profiler tests the results weren't too surprising, compared to my Pre-DOTS release physics wasn't an issue anymore
        and instead the Renderer actually had work that it could do.
        I assume it's due to the amount of drawcalls it's actually allowed to make now that the FPS is back.
        Instead my biggest enemy was two fold; 
                The EditorLoop, that would occassionaly spike compared to other Loops running at the same time
                and the Asteroid Initilization.
        
        The editor loop wasn't an issue after a test build though, showing that my GPU and CPU both got utilization levels between 30-40%
        Running 50k enemies before the FPS started dropping bellow the 60FPS-limit consistently
        
        Big FPS drops also happend if there were too many bullets on screen, since when Asteroids move it they also check for bullet collision.
        This means that the complexity of their calculations increase exponentially, as every Asteroid have to check the distance to every bullet in the scene.
        I'd imagine that creating a hashgrid of some kind would be useful here, letting every Asteroid only check the bullets that are close to it.
        But since I'm so unused to DOTS I don't know if this is a possibility or not. 

  ### Post-DOTS Summary 1.5f, Pre-Jobs, a side discussion (See TAG: DOTS-withJobs//code not in final main release)
      Alright, I don't know if I did something wrong here.
      Since I've identified that the most notable FPS drops happened during initilization    
      I'd planned to start creating Parallel Scheduled Jobs for my heaviest processes which included:
            - Asteroid, Spawning, Killing and Moving
            - Bullet, Spawning, Killing and Moving 
      I did get the Jobs to work, and from my understanding Jobs are meant to optimize multithreaded operations even further.
      But.. I found that my performence after the Jobs had been implemented was worse than before.
      If it was because I implemented them poorly I don't know, but I decided to scrap them and revert to the end of the previous Tag.

  ### Post-DOTS Summary 2 (See TAG: DOTS-withoutJobs.v2)
      For optimizatizing the code I only did a few changes after my previous DOTS release and reverting my IJob changes.
      They were significant though and allowed me to save on thousands of calculations as the asteroids scaled up.
      The changes here mostly included pre-calculating repeated calculations, moving simple math variables from
      Queary loops too the start of the update, or baking them from the GameManager.
      Since those calculations only have to be read and not done again, it allowed me to have about 5k extra entities before I started
      noticing the same FPS drops as I had before.

      My biggest culprit was still the Asteroid Initilization as it would consistently be too much for my computer to handle,
      the small changes I made here wasn't code related but were related to the gameManager. By lowering my spawn cooldown
      and spawn rate for asteroids, it allowed Unity to run more smoothly as there were fewer Enteties to Initialize during
      the same frame.

      Preferably I would've prefered to have the spawning on an IJob or put it on a buffer, maybe adding an IComponent tag
      and set it's spawn position after it's been put in the level. Not really sure what the best idea here would be.
      
  ### Summary
      Unity DOTS, though complicated allowed me to use my computers hardware that normal Unity development doesn't allow.
      Maximizing the use of my CPU, GPU and memory by having me write my code in a data-oriented framework, which is later
      compiled into a highly efficient machine code using the Burst Compiler.

      From only being able to to handle about 3000 objects effectively, using the Entity system it allowed a first time
      ECS programmer to handle 50k enteties at around 60fps.
      
      I think that learning to utilize Unity DOTS is necessary for effective Unity Game Development and this small trial-run
      really has put into perspective how much of a computers hardware isn't utilized a majority of the time for our
      student projects.
      
      Learning to write within the DOTS framework was tough, especially since a lot of information is old and decrepit
      as it's still being developed. But data-proented programming isn't exclusive to Unity and as programmers we should
      always strive to optimize our work within whatever engine we're using.
