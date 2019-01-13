clc;
clear all;

%Dane Pomiarowe

xp = [1 1 0 0
      1 0 1 0];
dp = [0 1 1 0];

%Wielkoœci Sta³e
p = length(xp);
p_p = [1:p];
k = 2;

%Losowanie Centrum
ind_c = randperm(p,k);
ind_x=setdiff(p_p,ind_c);
p = p-k;
c = xp(:,ind_c);
x = xp(:,ind_x);
d = dp(:,ind_x);

t =(max(c)-min(c))/2;
sigma = t.^2/k; 
 
%Radialna Funkcja Bazowa Gaussa
phi = @(x,c) exp(-sqrt((x-c)*(x-c))./2./sigma.^2);
 
phi(x(2),c(1))

 %Wyznaczanie Macierzy Wag
for i = 1:length(x)
    for j = 1:length(c)
    Phi(i,j,:) = phi(x(i),c(j));
    end
end

Phi2(:,:) = Phi(:,:,1);

 w = inv(Phi2'*Phi2)*Phi2'*d';
 
 %Wyznaczanie odpowiedzi sieci
 for i = 1:length(xp)
 
  for j = 1:k
      y0(i,:)= phi(xp(i),c(j))
     y(i,j) = w(j)*y0(i,1)
  end
  y2(i) = sum(y(i,:))
 end

