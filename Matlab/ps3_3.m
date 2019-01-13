clear all; close all;

%BAZY UCZACE

%  X = 0:1:10;
 
%  Y = [1 1.32 1.6 1.54 1.41 1.01 0.6 0.42 0.2 0.51 0.8];

X = 0:0.25:10;
Y = 0.8*sin(X/4) + 0.4*sin(pi*X/4) + 0.1*cos(pi*X);
  
  % !!! skalowanie danych -- funkcja sigmoidalna ma zakres (0,1), wiêc dane
  % równie¿ powinny mieœciæ siê w tym zakresie
  A = min(Y);          
  B = max(Y);
  Y = (Y-A)/(B-A);
     
   n=size(X,1);
   k1=10;
   k3=size(Y,1);
  
   beta = 1.5;
   eta = 0.15;
   a = -0.5;
   b = 0.5;
   
  % WSTEPNA INICJACJA MACIERZY WAG 
   W1=(b-a)*rand(n+1,k1)+a; %Warstwa ukryta (N+1 x K1)
   W2=(b-a)*rand(k1+1,k3)+a; %Warstwa wyjsciowa

   
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
    d1=W2(2:end,:)*d2; %d_j=w_1,j * e_1 + w_2,j*e2+...      % !!! zamieniono e2 na d2
    e1=beta*y1.*(1-y1).*d1; %f'(u1)*d1
    dW1=eta*x1*e1';
    %modyfikacja macierzy wag
    W1=W1+dW1;
    W2=W2+dW2;
   end
   
   figure;
   hold on;
   
  for i=1:size(Y,2)
   x1=[-1;X(:,i)]; % (N+1)x1  
   u1=W1' * x1;                 % !!! dodano u1
   y1 = 1./(1+exp(-beta*u1));
       x2=[-1;y1];
    u2=W2' * x2;
    y2 = 1./(1+exp(-beta*u2));

      du(:,i) = Y(:,i) - y2;
    
   plot(X(:,i),y2, 'r*');
   plot(X,Y);
    legend('odpowiedŸ sieci','obraz funkcji')
  end
  
     MSEU =  sum(sum(du.*du));

   
       