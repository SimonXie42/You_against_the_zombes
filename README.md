# You_against_the_zombes
Note about optimization: I noticed that whenever the player bumps into zombie(s), the game lags severely. Thus, I decided to fire up the profiler for some insights. Turns out FirstPersonController.FixedUpdate() is the major bottleneck, which I can’t really do anything about since it’s part of Unity’s standard assets. 

![snap](http://imsimonxie.com/images/profiler.PNG)



