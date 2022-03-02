using Raylib_cs;
using System;
using System.Numerics;
using static Raylib_cs.Raylib;
using static Raylib_cs.Texture2D;
using static Raylib_cs.KeyboardKey;
using static Raylib_cs.Color;


namespace HelloWorld
{
    public class Enemy{

        public int Xcoords;
        public int Ycoords;
        public int health; 
        public int originalHealth;
        public int searchRadius;
        public int speed;
        public int originX;
        public int originY;
        public int damage;
        public bool idle;
        public bool forward;

        public int distanceIdle = 50;

        public bool cooldown;

        public int cooldowntime;
        public int reinitcooldowntime;
        public Texture2D Texture;
        public Rectangle EnemySource;

        public void reAttack(){
            if(cooldown){
                cooldowntime--; 
                if(cooldowntime<=0){
                    cooldowntime = reinitcooldowntime;
                    cooldown = false;
                }               
            }

        }
        public void LoseHealth(int damage){
            health -= damage;
        }
        public void RessurectEnemy(){
            health = originalHealth;
            Xcoords = originX;
            Ycoords = originY;
            damage+=5;
        }

        public void NearPlayer(int PlayerX, int PlayerY){
            Vector2 EnemyVector = new Vector2(Xcoords,Ycoords);
            Vector2 PlayerVector = new Vector2(PlayerX,PlayerY);
            if(CheckCollisionCircles( EnemyVector,searchRadius ,PlayerVector, searchRadius )){
                idle = false;
                if(PlayerY==Ycoords){
                    if(Xcoords>PlayerX){
                        
                        if(Math.Abs(PlayerX-Xcoords)<speed){
                            Xcoords--;
                        }else{
                            Xcoords-=speed;
                        }
                        EnemySource.y=32;
                        if(EnemySource.x>=64){
                            EnemySource.x=-32;
                        }
                        EnemySource.x+=32;
                    }else if(Xcoords<PlayerX){
                        if(Math.Abs(PlayerX-Xcoords)<speed){
                            Xcoords++;
                        }else{
                            Xcoords+=speed;
                        }
                        //Xcoords+=speed;
                        EnemySource.y=64;
                        if(EnemySource.x>=64){
                            EnemySource.x=-32;
                        }
                        EnemySource.x+=32;
                    }else{
                    EnemySource.y=0;
                    EnemySource.x=32;
                    }
                }else if(PlayerY>Ycoords){
                    if(Math.Abs(PlayerY-Ycoords)<speed){
                        Ycoords++;
                    }else{
                        Ycoords+=speed;
                    }
                    EnemySource.y=0;
                    //Source.x=0; 
                    if(EnemySource.x>=64){
                        EnemySource.x=-32;
                    }
                    EnemySource.x+=32; 
                }else if(PlayerY<Ycoords){
                    if(Math.Abs(PlayerY-Ycoords)<speed){
                        Ycoords--;
                    }else{
                        Ycoords-=speed;
                    }
                    EnemySource.y=96;
                    if(EnemySource.x>=64){
                        EnemySource.x=-32;
                    }
                    EnemySource.x+=32;
                }else {
                    EnemySource.y=0;
                    EnemySource.x=32;
                }
            }else{
                    idle = true;
                    //EnemySource.y=0;
                    //EnemySource.x=32;

            }
        }
        public void IdlePlayer(int MaxX,int MaxY){
            if(distanceIdle==0){
                forward = !forward;
                distanceIdle = 50;
            }
                if(Xcoords>=MaxX){
                    Xcoords-=speed;
                        EnemySource.y=32;
                        if(EnemySource.x>=64){
                            EnemySource.x=-32;
                        }
                        EnemySource.x+=32;
                        forward = false;
                        distanceIdle--;
                }
                else if(Xcoords<=0){
                    forward = true;
                    Xcoords +=speed;
                    distanceIdle--;
                    EnemySource.y=64;
                    if(EnemySource.x>=64){
                        EnemySource.x=-32;
                    }
                    EnemySource.x+=32;   
                }else if(forward){

                    Xcoords+=speed;
                    distanceIdle--;
                    forward = true;
                    EnemySource.y=64;
                    if(EnemySource.x>=64){
                        EnemySource.x=-32;
                    }
                    EnemySource.x+=32;  
                      
            }else{
                    Xcoords-=speed;
                    distanceIdle--;
                    forward = false;
                    EnemySource.y=32;
                    if(EnemySource.x>=64){
                        EnemySource.x=-32;
                    }
                    EnemySource.x+=32;                 
            }
               
       
            
        }


    };
}