rm -rf _builds _steps _projects _cache _temp
wercker build --git-domain github.com --git-owner yersans --git-repository teamservice
rm -rf _builds _steps _projects _cache _temp