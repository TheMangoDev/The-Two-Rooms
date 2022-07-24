using Godot;
using System;

public class playerController : KinematicBody2D
{
    private int speed = 600;

    private int gravity = 15000;
    
    private int jump = 1000;

    private int jumpHeight = 15000;

    private float friction = .1f;

    private float acceleration = .25f;

    private bool isInAir = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    { 
        
    }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
      Vector2 velocity = new Vector2();
      
      int direction = 0;

      if(Input.IsActionPressed("ui_left"))
      {
        direction -= 1;
        GetNode<AnimatedSprite>("AnimatedSprite").FlipH = true;
      }
      if(Input.IsActionPressed("ui_right"))
      {
        direction += 1;
        GetNode<AnimatedSprite>("AnimatedSprite").FlipH = false;
      }
      if (direction != 0)
      {
          velocity.x = Mathf.Lerp(velocity.x, direction * speed, acceleration);
          if(!isInAir)
            GetNode<AnimatedSprite>("AnimatedSprite").Play("run");
      }
      else
      {
          if(!isInAir)
            GetNode<AnimatedSprite>("AnimatedSprite").Play("idle");
          velocity.x = Mathf.Lerp(velocity.x, 0, friction);
      }
      

      if (IsOnFloor())
      {
        if (Input.IsActionJustPressed("jump"))
        {
          velocity.y -= jump;
          velocity.y = -jumpHeight;
          GetNode<AnimatedSprite>("AnimatedSprite").Play("jump");
          isInAir = true;
        }
        else
        {
          isInAir = false;
        }
      }
      velocity.y += gravity * delta;
      MoveAndSlide(velocity, Vector2.Up);
  }
}
