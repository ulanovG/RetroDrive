shader_type spatial;
render_mode unshaded;

//grid
uniform vec3 line_color: source_color = vec3(0.5, 0.5, 0.5);
uniform float zoom_x;
uniform float zoom_y;
uniform float thickness_x: hint_range(0.0, 0.5, 0.001);
uniform float thicknessCorrection_x;
uniform float powerCorrection_x: hint_range(0.0, 10.0, 1.0);
uniform float thickness_y: hint_range(0.0, 0.5, 0.001);
uniform float thicknessCorrection_y;
uniform float powerCorrection_y: hint_range(0.0, 10.0, 1.0);

//gradient
uniform vec3 glow_color: source_color = vec3(0.5, 0.5, 0.5);
uniform float glowSize_x;
uniform float glowPower_x: hint_range(0.0, 10.0, 1.0);
uniform float glowSize_y;
uniform float glowPower_y: hint_range(0.0, 10.0, 1.0);

//movment
uniform float shift_x;
uniform float shift_y;

void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
	vec2 uv = UV - 0.5;
	
	uv.x = uv.x * zoom_x;
	uv.y = uv.y * zoom_y;

	uv.x += shift_x;
	uv.y += shift_y;

	vec2 grid_uv = (cos(uv * TAU));
	
	float distanceCorrection_x = pow(((1.0 - UV.y) + abs(0.5 - UV.x)), powerCorrection_x) * thicknessCorrection_x;
	float distanceCorrection_y = pow(((1.0 - UV.y) + abs(0.5 - UV.x)), powerCorrection_y) * thicknessCorrection_y;

	vec3 color_grid_x = smoothstep(1.0 - (thickness_x + distanceCorrection_x), 1.0, grid_uv.x) * line_color;
	vec3 color_grid_y = smoothstep(1.0 - (thickness_y + distanceCorrection_y), 1.0, grid_uv.y) * line_color;


	float glow = (pow((1.0 - UV.y), glowPower_x) * glowSize_x) * (pow((0.5 - abs(0.5 - UV.x)), glowPower_y) * glowSize_y);
	vec3 color_glow = smoothstep(0.0, 1.0, glow) * glow_color;


	vec3 color_grid = max(color_grid_x, color_grid_y);
	vec3 color = max(color_grid, color_glow);

	ALBEDO = color;
}
