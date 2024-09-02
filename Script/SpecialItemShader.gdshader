shader_type canvas_item;

uniform vec4 SpriteColor:source_color=vec4(1.0);

void fragment() {
	vec4 texture_color=texture(TEXTURE,UV);
	texture_color.rgb=mix(texture_color.rgb,SpriteColor.rgb,0.7);
	COLOR=texture_color;
}


//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}
