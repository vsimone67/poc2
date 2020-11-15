kubectl delete secret appsettings-secret-mibprocessor --namespace fac
 
kubectl delete configmap appsettings-mibprocessor --namespace fac

kubectl create secret generic appsettings-secret-mibprocessor --namespace fac --from-file=../appsettings.secrets.json

kubectl create configmap appsettings-mibprocessor --namespace fac --from-file=../appsettings.json