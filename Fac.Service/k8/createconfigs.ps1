kubectl delete secret appsettings-secret-facservice --namespace fac
 
kubectl delete configmap appsettings-facservice --namespace fac

kubectl create secret generic appsettings-secret-facservice --namespace fac --from-file=../appsettings.secrets.json

kubectl create configmap appsettings-facservice --namespace fac --from-file=../appsettings.json