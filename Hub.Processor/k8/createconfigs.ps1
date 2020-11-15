kubectl delete secret appsettings-secret-hubprocessor --namespace fac
 
kubectl delete configmap appsettings-hubprocessor --namespace fac

kubectl create secret generic appsettings-secret-hubprocessor --namespace fac --from-file=../appsettings.secrets.json

kubectl create configmap appsettings-hubprocessor --namespace fac --from-file=../appsettings.json