#include "debouncer.h"

// Função para verificar debounce de condições de entrada (Ex: botões)
// Ex: entra 0000 0110 -> Condições [1] e [2] são verdadeiras (bits[2:3] = 1).
void debounce(uint8* condicoes, unsigned long* main_timer) {
	static unsigned long dbtimer = 0;			// Timer usado para debounce das ações
	static uint8 status = 0;					// Status das ações registradas
	static uint8 executado = 0;					// Executado ou não
	
	// Detecta mudança de status (EX: borda de subida de um botão)
	if (status.value != (*condicoes).value) {
		status = *condicoes;
		executado.value = 0;
		dbtimer = *main_timer;
	}
	
	// Encerra a função caso: tempo de debounce decorrido OU uma ação já tenha sido executada
	if ((*main_timer)-dbtimer < DEBOUNCE_DELAY || executado.value) {
		(*condicoes).value = 0;
		return;
	}
	
	executado.value = (*condicoes).value;
}