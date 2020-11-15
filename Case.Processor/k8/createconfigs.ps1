kubectl delete secret appsettings-secret-casesprocessor --namespace fac
 
kubectl delete configmap appsettings-casesprocessor --namespace fac

kubectl create secret generic appsettings-secret-casesprocessor --namespace fac --from-file=../appsettings.secrets.json

kubectl create configmap appsettings-casesprocessor --namespace fac --from-file=../appsettings.json