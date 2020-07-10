# fractal-landscape

University of Melbourne<br/>
COMP 30019 – Graphics & Interaction

Getting acquainted with basic Unity development by using fractals to automatically generate an interactive 3D landscape

Objectives: 
+ Automatically generate randomly seeded fractal landscape at each invocation of the program (_diamond-square algorithm_)
+ Camera motion controls implemented in a "flight simulator" style
  * Mouse movement controls pitch and yaw
  * "W", "A", "S", "D" keys control movement
  * "Q", "E" keys control roll
+ Surface properties correspond in a sensible fashion according to the geometry of the terrain
+ Lighting based on _Phong illumination model_, with diffuse, specular and ambient components, using a custom Cg/HLSL shader
+ 30 FPS or greater
