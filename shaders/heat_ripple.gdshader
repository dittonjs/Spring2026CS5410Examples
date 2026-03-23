shader_type canvas_item;

uniform sampler2D screen_texture : hint_screen_texture, filter_linear;
uniform sampler2D noise_texture : repeat_enable;
uniform float distortion_strength : hint_range(0.0, 0.05) = 0.01;
uniform float scroll_speed : hint_range(0.0, 2.0) = 0.4;
uniform float noise_scale : hint_range(0.1, 10.0) = 3.0;

void fragment() {
    // Scroll the noise upward over time to simulate rising heat
    vec2 noise_uv = UV * noise_scale + vec2(0.0, TIME * scroll_speed);

    // Sample two channels of noise for x and y distortion
    vec2 noise_value = texture(noise_texture, noise_uv).rg;

    // Remap from [0, 1] to [-1, 1] so distortion goes in both directions
    vec2 distortion = (noise_value - vec2(0.5)) * 2.0 * distortion_strength;

    // Apply distortion to screen UV and sample
    COLOR = texture(screen_texture, SCREEN_UV + distortion);
}