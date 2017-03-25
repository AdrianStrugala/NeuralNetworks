clear all; 

%BAZY UCZACE
baza_ucz_we = ...
    [4 -1 -1 0;
     2 1 2 -1;
     0 1 -1 2];
 
 baza_ucz_wy = ...
     [1 0 0;
      0 1 0;
      0 0 1];
  
  baza_ucz_we=baza_ucz_we';
  baza_ucz_wy=baza_ucz_wy';
  
  n = size(baza_ucz_we,1);
  k = size(baza_ucz_we,2);
  
  beta = 1;
  eta = 0.1;
  a = -0.1;
  b = 0.1;
  
  % WSTEPNA INICJACJA MACIERZY WAG
  W = (b-a)*rand(n,k)+a;  %liczby z zakresu a..b
  
  Epoki = 3000;
  L = size(baza_ucz_we,2);
  
  for ep = 1:Epoki
  
      l=randi([1 L],1);
  x = baza_ucz_we(:,l);
  
  %OBLICZENIE ODPOWIEDZI SIECI
  u = W' * x;
  y = 1./(1+exp(-beta*u));
  
  % OBLICZENIE ROZNICY
  d = baza_ucz_wy(:,l) - y;
  
  %MODYFIKACJA MACIERZY WAG
  dW = eta * x * d';
  W = W + dW;
  end
  
 % x = [2 -1 2 -1];
  x = [4 1 -1 1]
  u = W' * x';
  y = 1./(1+exp(-beta*u))
  
  
  