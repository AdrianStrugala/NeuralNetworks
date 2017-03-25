clear all; 

MSEW_min = 10;

%BAZY UCZACE

baza_danych_we = ...
    [% cz�?on proporcjonalny
2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 2.0000 ;
1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 1.0000 ;
0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 0.5000 ;
% cz�?on ca�?kuj�?cy
0.0000 0.0100 0.0200 0.0300 0.0400 0.0500 0.0600 0.0700 0.0800 0.0900 0.1000 0.1100 0.1200 0.1300 0.1400 0.1500 0.1600 0.1700 0.1800 0.1900 0.2000 0.2100 0.2200 0.2300 0.2400 0.2500 0.2600 0.2700 0.2800 0.2900 0.3000 0.3100 0.3200 0.3300 0.3400 0.3500 0.3600 0.3700 0.3800 0.3900 0.4000 0.4100 0.4200 0.4300 0.4400 0.4500 0.4600 0.4700 0.4800 0.4900 0.5000 ;
0.0000 0.0250 0.0500 0.0750 0.1000 0.1250 0.1500 0.1750 0.2000 0.2250 0.2500 0.2750 0.3000 0.3250 0.3500 0.3750 0.4000 0.4250 0.4500 0.4750 0.5000 0.5250 0.5500 0.5750 0.6000 0.6250 0.6500 0.6750 0.7000 0.7250 0.7500 0.7750 0.8000 0.8250 0.8500 0.8750 0.9000 0.9250 0.9500 0.9750 1.0000 1.0250 1.0500 1.0750 1.1000 1.1250 1.1500 1.1750 1.2000 1.2250 1.2500 ;
0.0000 0.0500 0.1000 0.1500 0.2000 0.2500 0.3000 0.3500 0.4000 0.4500 0.5000 0.5500 0.6000 0.6500 0.7000 0.7500 0.8000 0.8500 0.9000 0.9500 1.0000 1.0500 1.1000 1.1500 1.2000 1.2500 1.3000 1.3500 1.4000 1.4500 1.5000 1.5500 1.6000 1.6500 1.7000 1.7500 1.8000 1.8500 1.9000 1.9500 2.0000 2.0500 2.1000 2.1500 2.2000 2.2500 2.3000 2.3500 2.4000 2.4500 2.5000 ;
% cz�?on róşniczkuj�?cy
2.0000 1.6375 1.3406 1.0976 0.8987 0.7358 0.6024 0.4932 0.4038 0.3306 0.2707 0.2216 0.1814 0.1485 0.1216 0.0996 0.0815 0.0667 0.0546 0.0447 0.0366 0.0300 0.0246 0.0201 0.0165 0.0135 0.0110 0.0090 0.0074 0.0061 0.0050 0.0041 0.0033 0.0027 0.0022 0.0018 0.0015 0.0012 0.0010 0.0008 0.0007 0.0005 0.0004 0.0004 0.0003 0.0002 0.0002 0.0002 0.0001 0.0001 0.0001 ;
2.0000 1.8097 1.6375 1.4816 1.3406 1.2131 1.0976 0.9932 0.8987 0.8131 0.7358 0.6657 0.6024 0.5451 0.4932 0.4463 0.4038 0.3654 0.3306 0.2991 0.2707 0.2449 0.2216 0.2005 0.1814 0.1642 0.1485 0.1344 0.1216 0.1100 0.0996 0.0901 0.0815 0.0738 0.0667 0.0604 0.0546 0.0494 0.0447 0.0405 0.0366 0.0331 0.0300 0.0271 0.0246 0.0222 0.0201 0.0182 0.0165 0.0149 0.0135 ;
1.0000 0.9512 0.9048 0.8607 0.8187 0.7788 0.7408 0.7047 0.6703 0.6376 0.6065 0.5769 0.5488 0.5220 0.4966 0.4724 0.4493 0.4274 0.4066 0.3867 0.3679 0.3499 0.3329 0.3166 0.3012 0.2865 0.2725 0.2592 0.2466 0.2346 0.2231 0.2122 0.2019 0.1920 0.1827 0.1738 0.1653 0.1572 0.1496 0.1423 0.1353 0.1287 0.1225 0.1165 0.1108 0.1054 0.1003 0.0954 0.0907 0.0863 0.0821 ];

 baza_danych_wy = ...
     [1 0 0;
      1 0 0;
      0 1 0;
      1 0 0; %ssak
      0 1 0;
      0 0 1; %ryba
      0 1 0; %ptak
      1 0 0;
      0 0 0; %inne
      
      1 0 0 0;
      0 0 1 0;
      0 1 0 0;
      0 1 0 0;
      1 0 0 0;
      0 1 0 0;
      0 0 1 0;
      0 0 1 0;
      0 0 0 1;
      0 0 1 0;
      1 0 0 0;
      0 1 0 0;
      0 1 0 0;
      0 0 1 0;
      0 0 1 0;
      0 1 0 0;
      0 0 1 0;
      0 1 0 0;
      1 0 0 0;
      0 0 0 1];
 
  i = size(baza_danych_we,1); %liczba przykladow
  we_prog = -1*ones(i,1);
  
  baza_danych_we = [we_prog baza_danych_we];
  
  baza_danych_we = baza_danych_we';
  baza_danych_wy = baza_danych_wy';
  
  n = size(baza_danych_we,1); %liczba cech +1(progowe)
  k = size(baza_danych_wy,1); %liczba kategorii
  

  losowe = randperm(i);
  ind = floor(i/3);
  ind_ucz = losowe(1:ind);
  ind_wal = losowe(ind:2*ind);
  ind_test = losowe(2*ind:3*ind);
  
  baza_ucz_we = baza_danych_we(:,ind_ucz);
  baza_ucz_wy = baza_danych_wy(:,ind_ucz);
  
  baza_wal_we = baza_danych_we(:,ind_wal);
  baza_wal_wy = baza_danych_wy(:,ind_wal);
  
  beta = 1;
  eta = 0.1;
  a = -0.1;
  b = 0.1;
  
  % WSTEPNA INICJACJA MACIERZY WAG
  W = (b-a)*rand(n,k)+a;  %liczby z zakresu a..b
  
  for j = 1:k
      W(1,j)=0.5;
  end
  
  Epoki = 10000;
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
  
  %OBLICZANIE BLEDU SREDNIOKWADRATOWEGO
        du=zeros(4,ind);
  
  for j = 1:ind
      
      iu=ind_ucz(1,j); 
      x = baza_ucz_we(:,j);
      u = W' * x;
      y = 1./(1+exp(-beta*u));
      du(:,j) = baza_ucz_wy(:,j) - y;
  end
  MSEU =  sum(sum(du.*du));
  
    for j = 1:ind
      
      iu=ind_wal(1,j); 
      x = baza_wal_we(:,j);
      u = W' * x;
      y = 1./(1+exp(-beta*u));
      du(:,j) = baza_wal_wy(:,j) - y;
  end
  MSEW =  sum(sum(du.*du));
  
  if MSEW_min*1.1 < MSEW  %jesli blad wzrosnie o wiecej niz 10% minimalnego
      break               %przerwij uczenie sie
  end
      
  
  if MSEW_min > MSEW;   %mininalny blad zestawu walidacyjnego
      MSEW_min = MSEW;
  end
  
  hold on;
  plot(ep,MSEW, 'r*');
   plot(ep,MSEU, 'b*');
  
  end
  
  
  