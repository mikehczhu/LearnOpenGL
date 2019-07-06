#version 330 core

struct Material
{
	vec3 ambient;
	vec3 diffuse;
	vec3 specular;
	float skininess;
};
struct Light
{
	vec3 ambient;
	vec3 diffuse;
	vec3 specular;
	vec3 position;
};

uniform Material material;
uniform Light light;
uniform vec3 viewPos;

in vec3 normal;
in vec3 FragPos;

out vec4 FragColor;

void main()
{
	vec3 ambient = light.ambient * material.ambient;
	
	vec3 norm = normalize(normal);
	vec3 lightDir = normalize(light.position - FragPos);
	float diff = max(dot(lightDir,norm),0.0);
	vec3 diffuse = light.diffuse * diff * material.diffuse;
	
	vec3 viewDir = normalize(viewPos - norm);
	vec3 specDir = reflect(-lightDir,norm);
	float spec = pow(max(dot(viewDir,specDir),0.0),material.skininess);
	vec3 specular = light.specular * spec * material.specular;
	
	vec3 result = ambient + diffuse + specular;
	
	FragColor = vec4(result,1.0);
}