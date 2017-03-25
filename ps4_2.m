clc;
clear all;

%Dane Pomiarowe
xp = 0:0.25:10;
dp = 0.8*sin(xp/4) + 0.4*sin(pi*xp/4) + 0.1*cos(pi*xp);

%xp = [1 1 0 0
 %     1 0 1 0];
%dp = [0 1 1 0];

%Wielkoúci Sta≥e
p = length(xp);
p_p = [1:p];
k = 10;

%Losowanie Centrum
ind_c = randperm(p,k);
ind_x=setdiff(p_p,ind_c);
p = p-k;
c = xp(:,ind_c);
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
 legend('odpowiedü sieci','obraz funkcji')
 
 for i = 1:length(xp)
    d_d(i) = dp(i)-y2(i); 
 end
 MSMSE = sum(d_d.*d_d);
