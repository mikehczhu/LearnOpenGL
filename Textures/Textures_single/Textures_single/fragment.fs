#version 330 core

in vec2 texCoord;
in vec3 ourColor;
out vec4 FragColor;

uniform sampler2D texure;

void main()
{
	FragColor = texture(texure,texCoord)*vec4(ourColor,1.0);
}