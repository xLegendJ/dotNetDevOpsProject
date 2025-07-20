# Creates ResourceGroup
az group create --name myresourcegroup --location centralus

# Creates Azure container registry
az acr create --resource-group myresourcegroup --name shoppingacr28394 --sku Basic

# Enables admin user in ACR
az acr update -n shoppingacr28394 --admin-enabled true

# Logins to the ACR
az acr login --name shoppingacr28394

# Gets Login server name
az acr list --resource-group myresourcegroup --query "[].{acrLoginServer:loginServer}" --output table

# Tags the image and prepares it to be pushed to our ACR
docker tag shoppingapi:latest shoppingacr28394.azurecr.io/shoppingapi:v1

# Tags the image and prepares it to be pushed to our ACR
docker tag shoppingclient:latest shoppingacr28394.azurecr.io/shoppingclient:v1

# Pushes image to our ACR
docker push shoppingacr28394.azurecr.io/shoppingapi:v1

# Pushes image to our ACR
docker push shoppingacr28394.azurecr.io/shoppingclient:v1

# List out images in our ACR
az acr repository list --name shoppingacr28394 --output table

# Shows tag of a specified image
az acr repository show-tags --name shoppingacr28394 --repository shoppingclient --output table

# Creates AKS cluster and attaches specified ACR
az aks create --resource-group myresourcegroup --name myAKSCluster --node-count 1 --generate-ssh-keys --attach-acr shoppingacr28394

#
kubectl config get-contexts

#
kubectl config use-contexts ${cluster name}

# Installs kubernetes cli to enable using kubectl commands
az aks install-cli

#
az aks get-credentials --resource-group myresourcegroup --name myAKSCLuster

# Creates image pull secrect for ACR
kubectl create secret docker-registry acr-secret --docker-server=shoppingacr28394.azurecr.io --docker-username=shoppingacr28394 --docker-password=${Password}

# Shows aks kubernetes version
az aks show --resource-group myresourcegroup --name myAKSCluster --query kubernetesVersion --output table

#
