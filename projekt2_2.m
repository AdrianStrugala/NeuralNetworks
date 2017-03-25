close all; clc; clear all;

P = 400;    % Liczba punktów pomiarowych
N = 1;      % Liczba wejœæ
K = 70;     % Liczba neuronów/wêz³ów w sieci

alpha = 0.8;  % Wspó³czynnik uczenia
lambda = 0.4;   % Promieñ s¹siedztwa

Epoki = 3000; % Liczba epok
ep = 1/Epoki;  % Czêstotliwoœæ zmian w 1 epoce

% Norma euklidesowa
dist = @(v1,v2) sqrt(sum((v2-v1).^2));
% Funkcja s¹siedztwa
neighbor = @(d,lam) (d<lam).*1;

% TWORZENIE KLASTRA DANYCH W PRZESTRZENI

xp = [0.0 0.1 0.2 0.3 0.4 0.5 0.6 0.7 0.8 0.9 1.0 1.1 1.2 1.3 1.4 1.5 1.6 1.7 1.8 1.9 2.0 2.1 2.2 2.3 2.4 2.5 2.6 2.7 2.8 2.9 3.0 3.0 3.1 3.2 3.3 3.4 3.5 3.6 3.7 3.8 3.9 4.0 4.1 4.2 4.3 4.4 4.5 4.6 4.7 4.8 4.9 5.0 5.1 5.2 5.3 5.4 5.5 5.6 5.7 5.8 5.9 6.0 6.0 6.2 6.4 6.6 6.8 7.0 7.2 7.4 7.6 7.8 8.0 8.2 8.4 8.6 8.8 9.0 9.0 9.4 9.8 10.2 10.6 11.0 11.4 11.8];
dp = [1.0000 0.7587 0.5438 0.3670 0.2372 0.1601 0.1376 0.1685 0.2484 0.3699 0.5237 0.6989 0.8839 1.0668 1.2368 1.3841 1.5009 1.5817 1.6232 1.6250 1.5887 1.5185 1.4203 1.3014 1.1700 1.0347 0.9039 0.7853 0.6855 0.6097 0.5612 0.5612 0.5417 0.5509 0.5870 0.6463 0.7242 0.8150 0.9126 1.0107 1.1034 1.1853 1.2520 1.3002 1.3278 1.3341 1.3199 1.2870 1.2383 1.1775 1.1089 1.0371 0.9665 0.9013 0.8454 0.8015 0.7719 0.7575 0.7585 0.7741 0.8027 0.8418 0.8418 0.9399 1.0427 1.1257 1.1716 1.1731 1.1341 1.0676 0.9917 0.9249 0.8820 0.8707 0.8905 0.9340 0.9887 1.0409 1.0409 1.0943 1.0595 0.9814 0.9330 0.9492 1.0048 1.0463];

x=[xp;dp];
% TWORZENIE SIECI KOHONENA
% Zakres wartoœci wag pocz¹tkowych sieci
a = 0;        
b = 8.5;

for k=1:K
   W(k).w = (b-a)*rand(2,1)+a;% Inicjacja wektorów wag neuronów
   W(k).w(2) = W(k).w(2)/b+0.5;
end
Wp = W;

% Rezerwacja miejsca dla wektorów odleg³oœci
D = zeros(1,K);
Dz = zeros(1,K);

for i=1:Epoki

    % Losowanie wejœcia
    l = randi([1 K],1);

    % Obliczenie odleg³oœci neuronów od wejœcia
    for k=1:K
        D(k) = dist(W(k).w,x(:,l));
    end

    % Szukanie zwyciêzcy
    [val, z] = min(D);

    % Obliczenie odleg³oœci wszystkich neuronów od zwyciêzcy
    for k=1:K
        Dz(k) = dist(W(k).w,W(z).w);
    end

    % POPRAWKI WAG + S¥SIEDZTWO
     for k=1:K
         W(k).w = W(k).w + alpha*neighbor(Dz(k),lambda)*(x(:,l)-W(k).w);
     end

    % Redukcja parametrów
    alpha = (1-ep)*alpha;
    lambda = (1-ep)*lambda;

end

% Plotowanie sieci PRZED
figure(1), hold on;
 plot(x(1,:),x(2,:),'g.','MarkerSize',18);
 axis([0 5 0 5]);
for k=1:K
   plot(Wp(k).w(1),Wp(k).w(2),'b.','MarkerSize',18);
end
axis([0 12 0 12]);

% Plotowanie sieci PO
 figure(2), hold on;
plot(x(1,:),x(2,:),'g.','MarkerSize',18);
 axis([0 12 0 12]);
for k=1:K
   plot(W(k).w(1),W(k).w(2),'r.','MarkerSize',18);
end
 axis([0 12 0 12]);
 
 %WYBIERANIE CENTRÓW
 count=0;
 c = W(1).w(1);
  for m=1:K
     if ~ismember(W(m).w(1),c) 
        count=count+1;
        c(count) = W(m).w(1);      
     end
  end
 

%Wielkoœci Sta³e
p = length(xp);
p_p = [1:p];
k = count; %liczba centrów

ind_x=setdiff(p_p,c);
p = p-k;
x = xp(:,ind_x);
d = dp(:,ind_x);

t = max(c)-min(c);
sigma = t^2/k; 
 
%Radialna Funkcja Bazowa Gaussa
phi = @(x,c) exp(-sqrt((x-c)*(x-c))/2/sigma^2);
 
 %Wyznaczanie Macierzy Wag
for i = 1:length(x)
    for j = 1:length(c)
    Phi(i,j) = phi(x(i),c(j));
    end
end
 w = inv(Phi'*Phi)*Phi'*d';
 
 %Wyznaczanie odpowiedzi sieci
 for i = 1:length(xp)
 
  for j = 1:k
     y(i,j) = w(j)*phi(xp(i),c(j))
  end
  y2(i) = sum(y(i,:))
 end

 
 figure;
 hold on;
 grid on;
 plot(xp,y2,'r');
 plot(xp,dp);
 legend('odpowiedŸ sieci','obraz funkcji')
 
 for i = 1:length(xp)
    d_d(i) = dp(i)-y2(i); 
 end
 MSMSE = sum(d_d.*d_d);
 
 
