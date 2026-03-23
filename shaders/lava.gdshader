shader_type canvas_item;
uniform sampler2D noise_texture : repeat_enable;
uniform float noise_scale = 0.1;

void vertex() {
    VERTEX += vec2(texture(noise_texture, UV + TIME * .1).r * 2.0 - 1.0, texture(noise_texture, UV + TIME * .1).r * 2.0 - 1.0) * .05; 
	// Called for every vertex the material is visible on.
}

void fragment() {
	// Called for every pixel the material is visible on.
    COLOR = vec4(1.0, texture(noise_texture, (UV + TIME * .05) * noise_scale).r, 0.0, 1.0);
}

//void light() {
//	// Called for every pixel for every light affecting the CanvasItem.
//	// Uncomment to replace the default light processing function with this one.
//}
