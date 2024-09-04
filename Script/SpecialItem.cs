using Godot;
using System;

public enum ItemEffect{
	None,
	IncreaseSpeed,
	DecreaseSpeed,
    BallDuplicate2,
	PlayerLengthIncrease,

}

public partial class SpecialItem : CharacterBody2D{
	[Export] float downSpeed=50f;
	[Export] Color firstColor;
	Node _ballManager;
	ItemEffect itemEffect=ItemEffect.None;

	public override void _PhysicsProcess(double delta)
	{
		if(itemEffect==ItemEffect.None){
			Sprite2D sprite=GetNode<Sprite2D>("Sprite2D");
			ShaderMaterial shaderMaterial=(ShaderMaterial)sprite.GetMaterial();
			shaderMaterial.SetShaderParameter("SpriteColor",firstColor);
			itemEffect=GetRndEffect();
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
