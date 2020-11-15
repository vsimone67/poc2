kubectl delete secret appsettings-secret-correspondenceprocessor --namespace fac
 
kubectl delete configmap appsettings-correspondenceprocessor --namespace fac

kubectl create secret generic appsettings-secret-correspondenceprocessor --namespace fac --from-file=../appsettings.secrets.json

kubectl create configmap appsettings-correspondenceprocessor --namespace fac --from-file=../appsettings.json