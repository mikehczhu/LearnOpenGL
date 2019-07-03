#version 330 core

in vec2 texCoord;
out vec4 Fragment;

uniform sampler2D texture1;
uniform sampler2D texture2;

uniform vec3 lightColor;

void main()
{
	Fragment = mix(texture(texture1,texCoord),texture(texture2,texCoord),0.2) * vec4(lightColor,1.0);
}