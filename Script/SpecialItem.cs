using Godot;
using System;
using System.Collections.Generic;

public enum ItemEffect{
	None,
	IncreaseSpeed,
	DecreaseSpeed,
    BallDuplicate2,
	PlayerLengthIncrease,
    PlayerLengthDecrease,
}

public partial class SpecialItem : CharacterBody2D{
	[Export] float downSpeed=50f;
	[Export] Color firstColor;
	Node _ballManager;
	ItemEffect itemEffect=ItemEffect.None;
	Dictionary<ItemEffect,Vector2> spriteDict=new Dictionary<ItemEffect, Vector2>{ //coordinate is base on the item sprite
		{ItemEffect.IncreaseSpeed,new Vector2(1,0)},
		{ItemEffect.DecreaseSpeed,new Vector2(2,0)},
		{ItemEffect.BallDuplicate2,new Vector2(3,0)},
		{ItemEffect.PlayerLengthIncrease,new Vector2(4,0)},
		{ItemEffect.PlayerLengthDecrease,new Vector2(5,0)},

	};
	const int WIDTH=16;
	const int HEIGHT=16;

	public override void _PhysicsProcess(double delta)
	{
		if(itemEffect==ItemEffect.None){
			Sprite2D sprite=GetNode<Sprite2D>("Sprite2D");
			ShaderMaterial shaderMaterial=(ShaderMaterial)sprite.GetMaterial();
			shaderMaterial.SetShaderParameter("SpriteColor",firstColor);
			itemEffect=GetRndEffect();
			if(itemEffect==ItemEffect.None)
				this.QueueFree();
			var coordinate=spriteDict[itemEffect];
			sprite.RegionRect=new Godot.Rect2(coordinate.X*WIDTH,coordinate.Y*HEIGHT,WIDTH,HEIGHT);
		}
		Vector2 velocity = Velocity;
		velocity.Y=downSpeed;
		Velocity=velocity;
		MoveAndSlide();
		for (int i = 0; i < GetSlideCollisionCount(); i++){
			var collision = GetSlideCollision(i);
			if(collision.GetCollider() is Player player){
				player.ReceiveEffectTrigger(this.itemEffect);
				GD.Print("I collided with ", ((Node)collision.GetCollider()).Name);
				this.QueueFree();
			}
		}
	}

	private ItemEffect GetRndEffect(){
		Array values = Enum.GetValues(typeof(ItemEffect));
		Random random = new Random();
		int randomIndex = random.Next(values.Length);
		return (ItemEffect)values.GetValue(randomIndex);
	}


}
