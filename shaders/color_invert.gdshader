shader_type canvas_item;

void fragment() {
    //vec4 og_color = texture(TEXTURE, UV);
    COLOR = vec4(1.0 - COLOR.r, 1.0 - COLOR.g, 1.0 - COLOR.b, COLOR.a);
	// Called for every pixel the material is visible on.
}

//void light() {
//	// Called for every pixel for every light affecting the CanvasItem.
//	// Uncomment to replace the default light processing function with this one.
//}
