close all; clc; clear all;

P = 400;    % Liczba punktów pomiarowych
N = 1;      % Liczba wejœæ
K = 50;     % Liczba neuronów/wêz³ów w sieci

alpha = 0.8;  % Wspó³czynnik uczenia
lambda = 1.3;   % Promieñ s¹siedztwa

Epoki = 3000; % Liczba epok
ep = 1/Epoki;  % Czêstotliwoœæ zmian w 1 epoce

% Norma euklidesowa
dist = @(v1,v2) sqrt(sum((v2-v1).^2));
% Funkcja s¹siedztwa
neighbor = @(d,lam) (d<lam).*1;

% TWORZENIE KLASTRA DANYCH W PRZESTRZENI

% ko³o

% 
% r = 4;
% t = linspace(0,2*pi,P);
% x0 = r*cos(t);
% y0 = r* sin(t);
% 
% r1 = 0:0.5:2;
% x1 = r*cos(t);
% y2 = r* sin(t);
% 
% x = [x0 reshape(x1,[],1)];
% y = [y0 reshape(y1,[],1)];
% 
r=4;
t=linspace(0.2*pi,P);
x=r*cos(t);
y=r*sin(t);
x=[x;y];
% TWORZENIE SIECI KOHONENA
% Zakres wartoœci wag pocz¹tkowych sieci
a = -5;        
b = 5;  
for k=1:K
   W(k).w = (b-a)*rand(2,1)+a;  % Inicjacja wektorów wag neuronów
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

    % POPRAWKI WAG - tylko ZWYCIÊZCA
%     W(z).w = W(z).w + alpha*(x(:,l)-W(z).w);

    % Redukcja parametrów
    alpha = (1-ep)*alpha;
    lambda = (1-ep)*lambda;

end

% Plotowanie sieci PRZED
figure(1), hold on;
 plot(x(1,:),x(2,:),'g.','MarkerSize',18);
 axis([-5 5 -5 5]);
for k=1:K
   plot(Wp(k).w(1),Wp(k).w(2),'b.','MarkerSize',18);
end
axis([-5 5 -5 5]);

% % Plotowanie sieci PO
 figure(2), hold on;
plot(x(1,:),x(2,:),'g.','MarkerSize',18);
 axis([-5 5 -5 5]);
for k=1:K
   plot(W(k).w(1),W(k).w(2),'r.','MarkerSize',18);
end
 axis([-5 5 -5 5]);
%     