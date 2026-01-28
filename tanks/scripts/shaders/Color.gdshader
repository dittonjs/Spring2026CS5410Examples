shader_type canvas_item;

void fragment() {
    COLOR = vec4(COLOR.b, COLOR.g, COLOR.r, COLOR.a);
}

