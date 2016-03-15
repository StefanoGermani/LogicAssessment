# LogicAssessment

To run the console application you need to run Visual Studio as administrator or manually reserve the port:

```
add urlacl url=http://+:1234/ user=DOMAIN\user
```

https://msdn.microsoft.com/en-us/library/windows/desktop/cc307223(v=vs.85).aspx

### NOTES


- the FakeUserRepository class is not thread safe
- the test on the password expiration is waiting for 30 secs... it could be improved by wrapping the DateTime and injecting it into the FakeUserRepository so that I can mock it in a unit test
