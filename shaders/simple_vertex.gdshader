shader_type canvas_item;

void vertex() {
    VERTEX += vec2(0.0, sin(TIME * 5.0 + UV.x) * .05);
	// Called for every vertex the material is visible on.
}

//void light() {
//	// Called for every pixel for every light affecting the CanvasItem.
//	// Uncomment to replace the default light processing function with this one.
//}
