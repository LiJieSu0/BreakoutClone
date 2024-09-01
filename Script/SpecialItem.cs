using Godot;
using System;

public partial class SpecialItem : CharacterBody2D{
	[Export] float downSpeed=50f;
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		velocity.Y=downSpeed;
		// if(collision!=null){
		// 	this.QueueFree();
		// 	//TODO add special effect
		// }
		Velocity=velocity;
		MoveAndSlide();
		for (int i = 0; i < GetSlideCollisionCount(); i++){
			var collision = GetSlideCollision(i);
			GD.Print("I collided with ", ((Node)collision.GetCollider()).Name);
		}
	}
}
