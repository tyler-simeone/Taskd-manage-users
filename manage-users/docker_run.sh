#!/bin/zsh

# Load environment variables from .env 
source .env

# Run the Docker container with specified environment variables and port mapping
docker run -d \
  --name manage-users \
  -p 5222:80 \
  -e UserPoolId=$USER_POOL_ID \
  -e Region=$REGION \
  -e LocalDBConnection=$LOCAL_DB_CONX \
  tylersimeone/projectb/manage-users:latest

if [ $? -ne 0 ]; then
  echo "Docker run command failed!"
  exit 1
fi

echo "Docker container started successfully."