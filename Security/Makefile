buildOutput=obj/CTFChallenge.Security.o
buildOpts:=
build:
	gcc -c ${buildOpts} CTFChallenge.Security.c -o ${buildOutput}

dllOutput:=bin/Debug/netstandard2.1/x64/CTFChallenge.Security.dll
dllOpts:=
dll: build
	 gcc -shared -o ${dllOutput} ${dllOpts} ${buildOutput}
