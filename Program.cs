using Raylib_cs;
using System;
using System.Numerics;
using static Raylib_cs.Raylib;
using static Raylib_cs.Texture2D;
using static Raylib_cs.KeyboardKey;
using static Raylib_cs.Color;

namespace HelloWorld
{
    static class Program
    {

        public unsafe static  void Main(string[] args)
        {
            
            int Length = 1000;
            int Width = 1000;
            int SizeLength = 800;
            int SizeWidth = 800;
            int PlayerX=100;
            int PlayerY=3;
            int score = 0;
            bool StartScreen = true;
            bool EndScreen = false;
            Rectangle Source = new Rectangle(32,0,32,32);






            Raylib.InitWindow(SizeLength, SizeWidth, "Z0mb13");

            Image Background = LoadImage("assets/finalmap.png"); 
            Image* BackgroundPointer = &Background;
            Raylib.ImageResize(BackgroundPointer, 1000, 1000); 
            Texture2D mapTexture = LoadTextureFromImage(Background);
            UnloadImage(Background); // Unload image from RAM

            Image Player = LoadImage("assets/finalspritemap.png"); 
            Texture2D texture = LoadTextureFromImage(Player);
            UnloadImage(Player); // Unload image from RAM

            Image Enemy = LoadImage("assets/enemymap.png"); 
            Texture2D EnemyTexture = LoadTextureFromImage(Enemy);
            UnloadImage(Enemy); // Unload image from RAM

            Image BulletAsset = LoadImage("assets/bullet.png"); 
            Texture2D bulletTexture = LoadTextureFromImage(BulletAsset);
            UnloadImage(BulletAsset); // Unload image from RAM


            Image UpBulletAsset = LoadImage("assets/upbullet.png"); 
            Texture2D UpbulletTexture = LoadTextureFromImage(UpBulletAsset);
            UnloadImage(UpBulletAsset); // Unload image from RAM

            Image DownBulletAsset = LoadImage("assets/downbullet.png"); 
            Texture2D DownbulletTexture = LoadTextureFromImage(DownBulletAsset);
            UnloadImage(DownBulletAsset); // Unload image from RAM

            Image LeftbulletAsset = LoadImage("assets/leftbullet.png"); 
            Texture2D LeftbulletTexture = LoadTextureFromImage(LeftbulletAsset);
            UnloadImage(LeftbulletAsset); // Unload image from RAM
            Raylib.SetTargetFPS(60);

            int speed = 10;
            Camera2D camera = new Camera2D();
            camera.target = new Vector2(PlayerX + 20, PlayerY + 20);
            camera.offset = new Vector2(Width / 2, Length / 2);
            camera.rotation = 0.0f;
            camera.zoom = 1.0f;
            bool verticle = true;
            bool negative = true;
            int damage = 15;

            bool cooldown = false;
            int cooldowntime = 0;

            List<Bullet> BulletList = new List<Bullet>();
            List<Obstacle> ObstacleList = new List<Obstacle>();
            Obstacle House = new Obstacle();
            House.x = 330;
            House.y = 0;
            House.width = 370;
            House.height = 150;
            ObstacleList.Add(House);

            Obstacle TrainTracks = new Obstacle();
            TrainTracks.x = 0;
            TrainTracks.y = 425;
            TrainTracks.width = 1000;
            TrainTracks.height = 115;
            ObstacleList.Add(TrainTracks);

            List<Vortex> VortexList = new List<Vortex>();

            Vortex Vortex1 = new Vortex();
            Vortex1.x = 25;
            Vortex1.y = 300;
            Vortex1.DestinationX = 25;
            Vortex1.DestinationY = 700; 
            VortexList.Add(Vortex1);
            
            
            Vortex Vortex2 = new Vortex();
            Vortex2.x = 25;
            Vortex2.y = 600;
            Vortex2.DestinationX = 25;
            Vortex2.DestinationY = 200;
            VortexList.Add(Vortex2);


            Vortex Vortex3 = new Vortex();
            Vortex3.x = 900;
            Vortex3.y = 300;
            Vortex3.DestinationX = 900;
            Vortex3.DestinationY = 700;
            VortexList.Add(Vortex3);
            
            
            Vortex Vortex4 = new Vortex();
            Vortex4.x = 900;
            Vortex4.y = 600;
            Vortex4.DestinationX = 900;
            Vortex4.DestinationY = 200;
            VortexList.Add(Vortex4);
            bool Sprint = false;


            List<Enemy> EnemyList = new List<Enemy>();
            Enemy CurrentEnemy1 = new Enemy();
            CurrentEnemy1.Xcoords = 0;
            CurrentEnemy1.Ycoords = 0;
            CurrentEnemy1.originX = 0;
            CurrentEnemy1.originY = 0;
            CurrentEnemy1.health = 100;
            CurrentEnemy1.originalHealth = 100;
            CurrentEnemy1.searchRadius = 100;
            CurrentEnemy1.damage = 5;
            CurrentEnemy1.idle = true;
            CurrentEnemy1.speed = 8;
            CurrentEnemy1.forward = true;
            CurrentEnemy1.Texture = EnemyTexture;
            CurrentEnemy1.cooldown = false;
            CurrentEnemy1.cooldowntime = 50;
            CurrentEnemy1.reinitcooldowntime = 50;
            CurrentEnemy1.EnemySource = new Rectangle(32,0,32,32);
            EnemyList.Add(CurrentEnemy1);

            Enemy CurrentEnemy2 = new Enemy();
            CurrentEnemy2.Xcoords = Length-80;
            CurrentEnemy2.Ycoords = 0;
            CurrentEnemy2.originX = Length-80;
            CurrentEnemy2.originY = 0;
            CurrentEnemy2.health = 100;
            CurrentEnemy2.originalHealth = 100;
            CurrentEnemy2.searchRadius = 100;
            CurrentEnemy2.damage = 5;
            CurrentEnemy2.speed = 8;
            CurrentEnemy2.idle = true;
            CurrentEnemy2.forward = false;
            CurrentEnemy2.Texture = EnemyTexture;
            CurrentEnemy2.cooldown = false;
            CurrentEnemy2.cooldowntime = 50;
            CurrentEnemy2.reinitcooldowntime = 50;            
            CurrentEnemy2.EnemySource = new Rectangle(32,0,32,32);
            EnemyList.Add(CurrentEnemy2);

            Enemy CurrentEnemy3 = new Enemy();
            CurrentEnemy3.Xcoords = 40;
            CurrentEnemy3.Ycoords = Width-100;
            CurrentEnemy3.originX = 40;
            CurrentEnemy3.originY = Width-100;
            CurrentEnemy3.health = 100;
            CurrentEnemy3.originalHealth = 100;
            CurrentEnemy3.searchRadius = 100;
            CurrentEnemy3.damage = 5;
            CurrentEnemy3.speed = 8;
            CurrentEnemy3.idle = true;
            CurrentEnemy3.forward = false;
            CurrentEnemy3.Texture = EnemyTexture;
            CurrentEnemy3.cooldown = false;
            CurrentEnemy3.cooldowntime = 50;
            CurrentEnemy3.reinitcooldowntime = 50;   
            CurrentEnemy3.EnemySource = new Rectangle(32,0,32,32);
            EnemyList.Add(CurrentEnemy3);

            Enemy CurrentEnemy4 = new Enemy();
            CurrentEnemy4.Xcoords = Length-40;
            CurrentEnemy4.Ycoords = Width-100;
            CurrentEnemy4.originX = Length-40;
            CurrentEnemy4.originY = Width-100;
            CurrentEnemy4.health = 100;
            CurrentEnemy4.originalHealth = 100;
            CurrentEnemy4.searchRadius = 100;
            CurrentEnemy4.damage = 5;
            CurrentEnemy4.speed = 8;
            CurrentEnemy4.idle = true;
            CurrentEnemy4.forward = false;
            CurrentEnemy4.Texture = EnemyTexture;
            CurrentEnemy4.cooldown = false;
            CurrentEnemy4.cooldowntime = 50;
            CurrentEnemy4.reinitcooldowntime = 50; 
            CurrentEnemy4.EnemySource = new Rectangle(32,0,32,32);
            EnemyList.Add(CurrentEnemy4);
            int health = 100;


            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                if(StartScreen){
                Raylib.ClearBackground(Color.BLACK);

                Raylib.DrawText("Z0mb13 Land", Length/4, Width/8, 40, Color.WHITE);
                Raylib.DrawText("Instructions", Length/7, Width/6, 40, Color.YELLOW);
                Raylib.DrawText("SHIFT TO SPRINT, SPACE TO SHOOT", Length/9, Width/4, 30, Color.YELLOW);

                Raylib.DrawText("WASD TO MOVE", Length/6, Width/3, 30, Color.YELLOW);
                Raylib.DrawRectangle(Length/4, Width/2,Width/2, Length/10, Color.GREEN);
                Raylib.DrawText("START", Length/4, Width/2,40, Color.BLACK);
                if(Raylib.IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON))
                {
                    health = 100;
                    PlayerX = 100;
                    PlayerY = 3;
                    foreach(Enemy CurrentEnemy in EnemyList){
                        CurrentEnemy.Xcoords = CurrentEnemy.originX;
                        CurrentEnemy.Ycoords = CurrentEnemy.originY;
                    }
                    score=0;
                    if(CanClick(Length/4, Width/2,Length/10))
                    {
                        StartScreen=false;
                    }
                }
                }else if(EndScreen){
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.BLACK);

                Raylib.DrawText("You Died :(", Length/4, Width/4, 40, Color.WHITE);
 
                Raylib.DrawText("Final Score: "+Convert.ToString(score), Length/4, Width/3, 40, Color.WHITE);
 
                Raylib.DrawRectangle(Length/4, Width/2,Width/2, Length/10, Color.GREEN);
                Raylib.DrawText("Restart", Length/4, Width/2,40, Color.BLACK);
                if(Raylib.IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON))
                {
                    if(CanClick(Length/4, Width/2,Length/10))
                    {
                        StartScreen=true;
                        EndScreen=false;
                        // Closes the current process
                    }
                } 
                }                   
                else{
                    System.Threading.Thread.Sleep(50);
                    camera.target = new Vector2(PlayerX + 20, PlayerY + 20);
                    BeginMode2D(camera);
                    Console.WriteLine(PlayerY);
                    Rectangle Destination = new Rectangle(PlayerX,PlayerY,64,64);
                    Vector2 origin = new Vector2(0,0); 
                    Color CurrentColor = new Color(96,126,140,0);
                    Raylib.ClearBackground(CurrentColor);
                    Raylib.DrawTexture(mapTexture,0,0, Color.WHITE);
                    Raylib.DrawTexturePro(texture,Source,Destination,origin,0, Color.WHITE);
                    Rectangle PlayerRec = new Rectangle(PlayerX,PlayerY,32,32);
                    Raylib.DrawText( "KILLS: "+Convert.ToString(score), PlayerX-450, PlayerY-480, 40, WHITE);
                    Raylib.DrawText( "HEALTH: "+Convert.ToString(health), PlayerX, PlayerY-480, 40, WHITE);
                    if(health<=0){
                        EndScreen = true;
                    }

                        foreach(Enemy CurrentEnemy in EnemyList){
                            if(CurrentEnemy.idle){
                                CurrentEnemy.IdlePlayer(Length,Width);
                                CurrentEnemy.idle = false;
                            }else{
                                CurrentEnemy.NearPlayer(PlayerX,PlayerY);
                            }
                            Console.WriteLine(CurrentEnemy.Xcoords);
                            Console.WriteLine(CurrentEnemy.Ycoords);
                            Vector2 EnemyOrigin = new Vector2(CurrentEnemy.Xcoords,CurrentEnemy.Ycoords);
                            Rectangle EnemyDestination = new Rectangle(CurrentEnemy.Xcoords,CurrentEnemy.Ycoords,64,64);
                            Raylib.DrawTexturePro(CurrentEnemy.Texture,CurrentEnemy.EnemySource,EnemyDestination,origin,0, Color.WHITE);
                            Rectangle EnemyBorder = new Rectangle(CurrentEnemy.Xcoords,CurrentEnemy.Ycoords,32,32);
                                CurrentEnemy.reAttack();

                            if(CheckCollisionRecs(EnemyBorder,PlayerRec)){
                                if(!CurrentEnemy.cooldown){
                                    health-= CurrentEnemy.damage;
                                    Raylib.DrawText( "--"+Convert.ToString(CurrentEnemy.damage), CurrentEnemy.Xcoords, CurrentEnemy.Ycoords, 40, Raylib.Fade(RED,5));
                                    CurrentEnemy.cooldown = true;
                                }
                            }
                            if(CurrentEnemy.health<=0){
                                CurrentEnemy.RessurectEnemy();
                                score++;
                            }
                        }

                        foreach(Obstacle CurrentObstacle in ObstacleList){
                            Rectangle ObstacleRec = new Rectangle(CurrentObstacle.x,CurrentObstacle.y,CurrentObstacle.width,CurrentObstacle.height); 
                            Raylib.DrawRectangle(CurrentObstacle.x,CurrentObstacle.y,CurrentObstacle.width,CurrentObstacle.height,Color.BLANK);
                            if(Raylib.CheckCollisionRecs(ObstacleRec,PlayerRec)){
                                if(verticle){
                                    if(negative){
                                        PlayerY-=speed;
                                        if(Sprint){
                                            PlayerY-=5;
                                        }
                                    }else{
                                        PlayerY+=speed;
                                        if(Sprint){
                                            PlayerY+=5;
                                        }
                                    }
                                }else{
                                    if(negative){
                                        PlayerX+=speed;
                                        if(Sprint){
                                            PlayerX+=5;
                                        }
                                    }else{
                                        PlayerX-=speed;
                                        if(Sprint){
                                            PlayerX-=5;
                                        }                                    
                                    }
                                }
                             }

                            for(int i=0;i<BulletList.Count;i++){
                                Texture2D CurrentTexture = bulletTexture; 
                                int bulletspeed = BulletList[i].speed;
                                
                                if(BulletList[i].negative){
                                        bulletspeed = bulletspeed*-1;
                                        if(BulletList[i].verticle){
                                            CurrentTexture = DownbulletTexture;
                                            BulletList[i].Ycoords -= bulletspeed;
                                        }
                                        else{
                                        BulletList[i].Xcoords += bulletspeed; 
                                        CurrentTexture=LeftbulletTexture;
                                        }
                                    }else{
                                        if(BulletList[i].verticle){
                                            CurrentTexture = UpbulletTexture;
                                            BulletList[i].Ycoords -= bulletspeed;
                                        }
                                        else{
                                        BulletList[i].Xcoords += bulletspeed; 
                                        }                                
                                    }
                                    //96,126,140
                                Raylib.DrawTexture(CurrentTexture,BulletList[i].Xcoords,BulletList[i].Ycoords, Color.WHITE);
                                BulletList[i].duration--;
                                if(BulletList[i].duration<=0){
                                    BulletList.RemoveAt(i);  
                                    break;
                                }
                                Rectangle BulletRectange = new Rectangle(BulletList[i].Xcoords,BulletList[i].Ycoords,32,32);
                                if(Raylib.CheckCollisionRecs(ObstacleRec,BulletRectange)){
                                   BulletList.RemoveAt(i);   
                                   cooldown = false;
                                   break;
                                }

                                foreach(Enemy CurrentEnemy in EnemyList){
                                    Rectangle EnemyBorder = new Rectangle(CurrentEnemy.Xcoords,CurrentEnemy.Ycoords,32,32);
                                    if(CheckCollisionRecs(EnemyBorder,BulletRectange)){
                                        CurrentEnemy.LoseHealth(BulletList[i].damage);
                                        Raylib.DrawText( "--"+Convert.ToString(BulletList[i].damage), CurrentEnemy.Xcoords, CurrentEnemy.Ycoords, 40, Raylib.Fade(WHITE,10));
                                        BulletList.RemoveAt(i);  
                                        break; 
                                    }
                                }
                            }
                        }
                    if(PlayerY>=0&&PlayerX>=0&&PlayerX<=Length-40&&PlayerY<=950){
                        //bool Sprint = false;
                        for(int i=0;i<VortexList.Count;i++){
                            Rectangle VortexRectangle = new Rectangle(VortexList[i].x,VortexList[i].y,10,10);
                            if(Raylib.CheckCollisionRecs(PlayerRec,VortexRectangle)){
                                Console.WriteLine("HIT");
                                PlayerX = VortexList[i].DestinationX;
                                PlayerY = VortexList[i].DestinationY;
                            }
                        }
                        //Raylib.StopSound(WalkNoise);
                                                
                        if (IsKeyDown(KEY_S)){
                            verticle = true;
                            negative = true;
                            if(IsKeyDown(KEY_LEFT_SHIFT)){
                                Sprint = true;
                                PlayerY+=5;
                            } 
                            PlayerY+=speed;     
                            Source.y=0;
                            //Source.x=0; 
                            if(Source.x>=64){
                                Source.x=-32;
                            }
                            Source.x+=32;  

                        }else if(IsKeyDown(KEY_W)){
                           
                        verticle = true;
                        negative = false;
                        if(IsKeyDown(KEY_LEFT_SHIFT)){
                                Sprint = true;
                                PlayerY-=5;
                            } 
                            PlayerY-=speed; 
                            Source.y=96;
                            if(Source.x>=64){
                                Source.x=-32;
                            }
                            Source.x+=32;
                        }else if(IsKeyDown(KEY_D)){
                            negative = false;
                            verticle = false;
                            if(IsKeyDown(KEY_LEFT_SHIFT)){
                                Sprint = true;
                                PlayerX+=5;
                            } 
                            PlayerX+=speed; 
                            Source.y=64;
                            if(Source.x>=64){
                                Source.x=-32;
                            }
                            Source.x+=32;
                        }else if(IsKeyDown(KEY_A)){
                            negative = true;
                            if(IsKeyDown(KEY_LEFT_SHIFT)){
                                Sprint = true;
                                PlayerX-=5;
                            } 
                            verticle = false;
                            PlayerX-=speed; 
                            Source.y=32;
                            if(Source.x>=64){
                                Source.x=-32;
                            }
                            Source.x+=32;
                        }else {
                            
                            Console.WriteLine("WALKING AWAY");
                        }
                    }else if(PlayerX<0){

                        PlayerX++;
                    }else if(PlayerX>Length-40){

                        PlayerX--;
                    }else if(PlayerY<0){
                        PlayerY++;
                    }else if(PlayerY>Width-30){
                        PlayerY--;
                    }
                    else if(PlayerY>=950){
                        PlayerY--;
                    }else{

                    }
                    if(cooldown){
                        cooldowntime++;
                        if(cooldowntime==5){
                            cooldown = false;
                            cooldowntime = 0;
                        }
                    }else if(IsKeyDown(KEY_SPACE)){
                        cooldown = true;
                        Bullet CurrentBullet = new Bullet();
                        CurrentBullet.speed = speed+1;
                        CurrentBullet.damage = 15;
                        CurrentBullet.duration = 100;
                        CurrentBullet.verticle = verticle;
                        CurrentBullet.Xcoords = PlayerX;
                        CurrentBullet.Ycoords = PlayerY;
                        CurrentBullet.negative = negative;
                        BulletList.Add(CurrentBullet);
                    }

                    
             
                }
                Raylib.EndDrawing();
            }
            Raylib.UnloadImage(Player);
            Raylib.UnloadTexture(texture);
            Raylib.UnloadTexture(EnemyTexture);
            Raylib.UnloadTexture(UpbulletTexture);
            Raylib.UnloadTexture(bulletTexture);
            Raylib.UnloadTexture(DownbulletTexture);
            Raylib.UnloadTexture(LeftbulletTexture);

            CloseAudioDevice();
            Raylib.CloseWindow();
        }
    public static bool CanClick(int Length,int Width,int Height)
    {
        MouseInput mousePos = new MouseInput();    
        mousePos.X= (int)Raylib.GetMousePosition().X;
        mousePos.Y= (int)Raylib.GetMousePosition().Y;   // Get the position of the mouse
        Rectangle ButtonRect = new Rectangle(Length,Width,Width,Height);
        Vector2 point = new Vector2(mousePos.X, mousePos.Y);
        if(Raylib.CheckCollisionPointRec(point,ButtonRect)){
            return true;
        }
        

        return false;
    }
}
}