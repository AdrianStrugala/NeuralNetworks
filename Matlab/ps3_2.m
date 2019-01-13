clear all; 

%BAZY UCZACE

  X = 0:1:10;
 
  Y = sin(X/4) + cos(X);
  
  T = 0:0.1:10;
  Yt = sin(T/4) + cos(T);
  
  %X=X';
 % Y=Y';
   
   n=size(X,1);
   k1=2;
   k2=2;
   k3=size(Y,1);
  
   beta = 1;
   eta = 0.1;
   a = -0.1;
   b = 0.1;
   
  % WSTEPNA INICJACJA MACIERZY WAG 
   W1=(b-a)*rand(n+1,k1)+a; %Warstwa ukryta (N+1 x K1)
   W2=(b-a)*rand(k1+1,k2)+a; %Warstwa ukryta (2)
   W3=(b-a)*rand(k2+1,k3)+a; %Warstwa wyjœciowa
   
   Epoki = 100;
   
   for ep = 1:Epoki
   %LOSOWANIE PRZYK£ADU Z BAZY UCZ¥CEJ
   l=randi([1 size(X,2)],1);
   %DODANIE WEJSCIA BIASU
   x1=[-1;X(:,l)]; % (N+1)x1
   %OBLICZENIE POBUDZENIA 1. WARSTWY
   u1=W1' * x1;
   y1 = 1./(1+exp(-beta*u1));
   
%    %DODOAODOAOD
%    d1=Y(:,l)-y1;
%     dW1=eta*x1*d1';
%     W1=W1+dW1;
    
   %PRZEKAZANIE ODP. 1. WARSTWY NA WEJSCIE 2. WARSTWY
    x2=[-1;y1];
    %OBLICZENIE POBUDZENIE 2. WARSTWY
    u2=W2' * x2;
    y2 = 1./(1+exp(-beta*u2));
   
    x3=[-1;y2];
   u3=W3' * x3;
    y3 = 1./(1+exp(-beta*u3));
    
    d3=Y(:,l)-y3;
    e3=beta*y3.*(1-y3).*d3; %f'(u2)*d2
    dW3=eta*x3*e3';
    d2=W3(2:end,:)*e3; %d_j=w_1,j * e_1 + w_2,j*e2+...
    
    %WSTECZNA PROPAGACJA BLEDU
   %OBLICZENIE POPRAWEK DLA 2.WARSTWY
  %  d2=Y(:,l)-y2;
    e2=beta*y2.*(1-y2).*d2; %f'(u2)*d2
    dW2=eta*x2*e2';
    %OBLICZENIE POPRAWEK DLA 1. WARSTWY
    d1=W2(2:end,:)*e2; %d_j=w_1,j * e_1 + w_2,j*e2+...
    e1=beta*y1.*(1-y1).*d1; %f'(u1)*d1
    dW1=eta*x1*e1';
    %modyfikacja macierzy wag
    W1=W1+dW1;
    W2=W2+dW2;
    W3=W3+dW3;
   end
  
   y3
   
%    figure;
%    hold on;
%    
%   for i=1:10
%    x1=[-1;X(:,i)]; % (N+1)x1  
%    y1 = 1./(1+exp(-beta*u1));
%        x2=[-1;y1];
%     u2=W2' * x2;
%     y2 = 1./(1+exp(-beta*u2));
%            x3=[-1;y2];
%      u3=W3' * x3;
%     y3 = 1./(1+exp(-beta*u3));
% 
%    plot(i,y3, 'r*');
%    plot(X,Y);
%   end

   
       