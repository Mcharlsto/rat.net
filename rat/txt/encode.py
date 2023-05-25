import base64
import os

for file in os.listdir(os.getcwd()):
     filename = os.fsdecode(file)
     if filename.endswith(".txt"):
        print(filename)
        ins = open(filename,'rb').read()
        otp  = base64.b64encode(ins)
        open(filename,'wb').write(otp)
        #base64.encode(open(filename,'rb'), open(filename,'wb'))