clear all; 

%BAZY UCZACE

  X=[1 1 0 0;
     1 0 1 0];
 
  Y=[0 1 1 0];
  
  
  n=size(X,1);
  k1=2;
  k2=size(Y,1);
  
  beta = 1;
  eta = 0.1;
  a = -0.1;
  b = 0.1;
  
  % WSTEPNA INICJACJA MACIERZY WAG 
  W1=(b-a)*rand(n+1,k1)+a; %Warstwa ukryta (N+1 x K1)
  W2=(b-a)*rand(k1+1,k2)+a; %Warstwa wyjœciowa
  
  Epoki = 100000;
  
  for ep = 1:Epoki
   %LOSOWANIE PRZYK£ADU Z BAZY UCZ¥CEJ
   l=randi([1 size(X,2)],1);
   %DODANIE WEJSCIA BIASU
   x1=[-1;X(:,l)]; % (N+1)x1
   %OBLICZENIE POBUDZENIA 1. WARSTWY
   u1=W1' * x1;
   y1 = 1./(1+exp(-beta*u1));
   %PRZEKAZANIE ODP. 1. WARSTWY NA WEJSCIE 2. WARSTWY
   x2=[-1;y1];
   %OBLICZENIE POBUDZENIE 2. WARSTWY
   u2=W2' * x2;
   y2 = 1./(1+exp(-beta*u2));
   %WSTECZNA PROPAGACJA BLEDU
   %OBLICZENIE POPRAWEK DLA 2.WARSTWY
   d2=Y(:,l)-y2;
   e2=beta*y2.*(1-y2).*d2; %f'(u2)*d2
   dW2=eta*x2*e2';
   %OBLICZENIE POPRAWEK DLA 1. WARSTWY
   d1=W2(2:end,:)*e2; %d_j=w_1,j * e_1 + w_2,j*e2+...
   e1=beta*y1.*(1-y1).*d1; %f'(u1)*d1
   dW1=eta*x1*e1';
   %modyfikacja macierzy wag
   W1=W1+dW1;
   W2=W2+dW2;
  end
  


  y2;
  
  
  

   
       