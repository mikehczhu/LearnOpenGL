#version 330 core

struct Material
{
	sampler2D diffuse;
	sampler2D specular;
	float skininess;
};
struct Light
{
	vec3 ambient;
	vec3 diffuse;
	vec3 specular;
	vec3 position;
	vec3 direction;
	float cutOff;
	float outCutOff;
	float contant;
	float linear;
	float quadratic;
};

uniform Material material;
uniform Light light;

in vec3 normal;
in vec2 texCoord;
in vec3 FragPos;


out vec4 FragColor;

void main()
{
	vec3 ambient = light.ambient * vec3(texture(material.diffuse,texCoord));
	
	vec3 norm = normalize(normal);
	vec3 lightDir = normalize(light.position - FragPos);
	float diff = max(dot(norm,lightDir),0.0);
	vec3 diffuse = light.diffuse * diff * vec3(texture(material.diffuse,texCoord));
	
	vec3 viewDir = normalize(light.position - FragPos);
	vec3 specDir = reflect(-lightDir,norm);
	float spec = pow(max(dot(viewDir,specDir),0.0),material.skininess);
	vec3 specular = light.specular * spec * vec3(texture(material.specular,texCoord));
	
	float theta = dot(lightDir,-light.direction);
	float epsilon = light.cutOff - light.outCutOff;
	float intension = clamp((theta - light.outCutOff)/epsilon,0.0,1.0);
	
	diffuse *= intension;
	specular *= intension;
	
	float distance = length(light.position - FragPos);
	float attenuation = 1.0/(light.contant+light.linear*distance+light.quadratic*distance*distance);
	
	ambient *= attenuation;
	diffuse *= attenuation;
	specular *= attenuation;
	
	vec3 result = ambient + diffuse + specular;
	
	FragColor = vec4(result,1.0);
}