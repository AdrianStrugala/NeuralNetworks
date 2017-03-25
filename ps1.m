clear all; 

%BAZY UCZACE
baza_ucz_we = ...
    [4 2 2 4;
     1 1 0 0;
     0 0 1 0;
     5 2 1 10];
 
 baza_ucz_wy = ...
     [1 0 0 0;
      0 1 0 0;
      0 0 1 0;
      0 0 0 1];
  
  n = size(baza_ucz_we,1);
  k = size(baza_ucz_we,1);
  
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
  
  
  