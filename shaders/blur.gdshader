shader_type canvas_item;

uniform float blur_distance = 0.04;

void fragment() {
    COLOR += texture(TEXTURE, vec2(UV.x, UV.y - blur_distance));
    COLOR += texture(TEXTURE, vec2(UV.x, UV.y + blur_distance));
    COLOR += texture(TEXTURE, vec2(UV.x + blur_distance, UV.y));
    COLOR += texture(TEXTURE, vec2(UV.x - blur_distance, UV.y));
    COLOR /= 5.0;
}

//void light() {
//	// Called for every pixel for every light affecting the CanvasItem.
//	// Uncomment to replace the default light processing function with this one.
//}
