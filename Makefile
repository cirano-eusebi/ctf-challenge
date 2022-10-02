CLI:=docker
TAG:=ceusebi/ctf-challenge

build:
	${CLI} build -t ${TAG} -f Server/Dockerfile .

PORT:=8080
run: build
	${CLI} run --rm -p ${PORT}:80 ${TAG}

push: build
	${CLI} push ${TAG}
