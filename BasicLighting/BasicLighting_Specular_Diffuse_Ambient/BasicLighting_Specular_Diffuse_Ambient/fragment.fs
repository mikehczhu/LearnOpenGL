#version 330 core

in vec2 texCoord;
in vec3 normal;
in vec3 FragPos;

out vec4 FragColor;

uniform vec3 lightColor;
uniform vec3 lightPos;
uniform vec3 viewPos;

uniform sampler2D texture1;
uniform sampler2D texture2;

void main()
{
	float ambientStrength = 0.1;
	vec3 ambient = ambientStrength * lightColor;
	
	vec3 norm = normalize(normal);
	vec3 lightDir = normalize(lightPos - FragPos);
	float diff = max(dot(norm,lightDir),0.0);
	vec3 diffuse = diff * lightColor;
	
	float specularStrength = 1.0;
	vec3 viewDir = normalize(viewPos - FragPos);
	vec3 specDir = reflect(-lightDir,norm);
	float spec = pow(max(dot(specDir,viewDir),0.0),32);
	vec3 specular = specularStrength * spec * lightColor;
	
	vec3 result = (ambient + diffuse + specular);
	
	FragColor = mix(texture(texture1,texCoord),texture(texture2,texCoord),0.2) * vec4(result,1.0);
}